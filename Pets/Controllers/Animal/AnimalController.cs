using System.Linq;

namespace Pets.Controllers.Animal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.Globalization;
    using System.Threading.Tasks;
    using Add;
    using Feed;
    using Get;
    using GetList;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Providers;
    using Domain.Enums;
    using Domain.Entities;
    using Domain.Services;
    using Domain.ValueObjects;

    [ApiController]
    [Route("api/animal")]
    public class AnimalController : Controller
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";


        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(AnimalGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList(AnimalGetListRequest request)
        {
            List<Animal> animals = new List<Animal>();

            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand command = connection.CreateCommand();

            List<string> conditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                conditions.Add("a.Name LIKE '%' || @Search || '%'");
                command.Parameters.AddWithValue("Search", request.Search);
            }

            if (request.AnimalType.HasValue)
            {
                conditions.Add("a.Type = @AnimalType");
                command.Parameters.AddWithValue("AnimalType", request.AnimalType.Value);
            }

            command.CommandText = $@"
                SELECT
                    a.Id,
                    a.Type,
                    a.Name,
                    a.BreedId,
                    c.Weight,
                    d.TailLength
                FROM Animal a
                LEFT JOIN Cat c ON c.AnimalId = a.Id
                LEFT JOIN Dog d ON d.AnimalId = a.Id
                {(conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty)}
                ORDER BY a.Name";

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            IDictionary<long, Breed> breedCache = new Dictionary<long, Breed>();

            while (await reader.ReadAsync())
            {
                long breedId = reader.GetInt64("BreedId");

                if (!breedCache.ContainsKey(breedId))
                {
                    Breed breed = await GetBreedAsync(connection, breedId);

                    breedCache.Add(breedId, breed);
                }

                long animalId = reader.GetInt64("Id");

                IEnumerable<Feeding> feedings = await GetFeedingsAsync(connection, animalId);

                Animal animal = CreateAnimalFromReader(reader, breedCache[breedId], feedings);

                animals.Add(animal);
            }

            await transaction.CommitAsync();

            AnimalGetListResponse response = new AnimalGetListResponse()
            {
                Animals = animals,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(AnimalGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(AnimalGetRequest request)
        {
            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            Animal animal = await GetAnimalAsync(connection, request.Id);
            
            await transaction.CommitAsync();

            AnimalGetResponse response = new AnimalGetResponse()
            {
                Animal = animal,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(AnimalAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AnimalAddRequest request)
        {
            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction =
                await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            Animal animal;

            Breed breed = await GetBreedAsync(connection, request.BreedId);

            switch (request.Type)
            {
                case AnimalType.Cat:
                    var catService = new CatService(connection);

                    animal = await catService.CreateCatAsync(
                        name: request.Name.Trim(),
                        breed: breed,
                        weight: request.Weight ?? throw new ArgumentNullException(nameof(request.Weight)));

                    break;

                case AnimalType.Dog:

                    var dogService = new DogService(connection);

                    animal = await dogService.CreateDogAsync(
                        name: request.Name.Trim(),
                        breed: breed,
                        tailLength: request.TailLength ?? throw new ArgumentNullException(nameof(request.TailLength)));

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(request.Type), request.Type, "Unknown animal type");
            }

            await using SQLiteCommand insertAnimalCommand = connection.CreateCommand();

            insertAnimalCommand.CommandText = @"
                INSERT INTO Animal
                (
                    Type,
                    Name,
                    BreedId
                )
                VALUES 
                (
                    @Type,
                    @Name,
                    @BreedId
                ); SELECT last_insert_rowid()";

            insertAnimalCommand.Parameters.AddWithValue("Type", (int) animal.Type);
            insertAnimalCommand.Parameters.AddWithValue("Name", animal.Name);
            insertAnimalCommand.Parameters.AddWithValue("BreedId", animal.Breed.Id);

            animal.Id = (long) insertAnimalCommand.ExecuteScalar();

            switch (request.Type)
            {
                case AnimalType.Cat:
                    var cat = animal as Cat;

                    await using (SQLiteCommand insertCatCommand = connection.CreateCommand())
                    {
                        insertCatCommand.CommandText = @"
                            INSERT INTO Cat
                            (
                                AnimalId,
                                Weight
                            )
                            VALUES
                            (
                                @AnimalId,
                                @Weight
                            )";
                        insertCatCommand.Parameters.AddWithValue("AnimalId", cat.Id);
                        insertCatCommand.Parameters.AddWithValue("Weight", cat.Weight);

                        insertCatCommand.ExecuteNonQuery();
                    }

                    break;

                case AnimalType.Dog:
                    var dog = animal as Dog;

                    await using (SQLiteCommand insertDogCommand = connection.CreateCommand())
                    {
                        insertDogCommand.CommandText = @"
                            INSERT INTO Dog
                            (
                                AnimalId,
                                TailLength
                            )
                            VALUES
                            (
                                @AnimalId,
                                @TailLength
                            )";
                        insertDogCommand.Parameters.AddWithValue("AnimalId", dog.Id);
                        insertDogCommand.Parameters.AddWithValue("TailLength", dog.TailLength);

                        await insertDogCommand.ExecuteNonQueryAsync();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(request.Type), request.Type, "Unknown animal type");
            }

            await transaction.CommitAsync();

            AnimalAddResponse response = new AnimalAddResponse()
            {
                Id = animal.Id,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("feed")]
        [ProducesResponseType(typeof(AnimalFeedResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Feed(AnimalFeedRequest request)
        {
            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            Animal animal = await GetAnimalAsync(connection, request.AnimalId);

            Food food = await GetFoodAsync(connection, request.FoodId);

            var feedingService = new FeedingService();

            feedingService.Feed(animal, food, request.Count);

            foreach (var feeding in animal.Feedings)
            {
                if (feeding.Id == default)
                {
                    await using SQLiteCommand insertFeedingCommand = connection.CreateCommand();

                    insertFeedingCommand.CommandText = @"
                        INSERT INTO Feeding
                        (
                            AnimalId,
                            FoodId,
                            DateTimeUtc,
                            Count
                        )
                        VALUES
                        (
                            @AnimalId,
                            @FoodId,
                            @DateTimeUtc,
                            @Count
                        );";

                    insertFeedingCommand.Parameters.AddWithValue("AnimalId", animal.Id);
                    insertFeedingCommand.Parameters.AddWithValue("FoodId", feeding.Food.Id);
                    insertFeedingCommand.Parameters.AddWithValue("DateTimeUtc", feeding.DateTimeUtc.ToString(DateTimeFormat));
                    insertFeedingCommand.Parameters.AddWithValue("Count", feeding.Count);

                    await insertFeedingCommand.ExecuteNonQueryAsync();
                }
            }

            await using SQLiteCommand updateFoodCommand = connection.CreateCommand();

            updateFoodCommand.CommandText = @"
                UPDATE Food 
                SET Count = @Count
                WHERE Id = @Id";

            updateFoodCommand.Parameters.AddWithValue("Id", food.Id);
            updateFoodCommand.Parameters.AddWithValue("Count", food.Count);

            await updateFoodCommand.ExecuteNonQueryAsync();

            await transaction.CommitAsync();

            AnimalFeedResponse response = new AnimalFeedResponse();

            return Json(response);
        }



        private static Animal CreateAnimalFromReader(DbDataReader reader, Breed breed, IEnumerable<Feeding> feedings)
        {
            Animal animal;

            long id = reader.GetInt64("Id");
            string name = reader.GetString("Name");
            AnimalType type = (AnimalType) reader.GetInt64("Type");

            switch (type)
            {
                case AnimalType.Cat:
                    animal = new Cat(
                        id: id,
                        name: name,
                        breed: breed,
                        weight: reader.GetDecimal("Weight"),
                        feedings: feedings);
                    break;

                case AnimalType.Dog:
                    animal = new Dog(
                        id: id,
                        name: name,
                        breed: breed,
                        tailLength: reader.GetDecimal("TailLength"),
                        feedings: feedings);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown animal type");
            }

            return animal;
        }



        private static async Task<Breed> GetBreedAsync(SQLiteConnection connection, long id)
        {
            await using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    b.Id,
                    b.Name,
                    b.AnimalType
                FROM Breed b
                WHERE b.Id = @Id";

            command.Parameters.AddWithValue("Id", id);

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Breed breed = new Breed(
                    id: reader.GetInt64("Id"),
                    animalType: (AnimalType)reader.GetInt64("AnimalType"),
                    name: reader.GetString("Name"));

                return breed;
            }

            return null;
        }

        private static async Task<Food> GetFoodAsync(SQLiteConnection connection, long id)
        {
            await using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    f.Id,
                    f.Name,
                    f.AnimalType,
                    f.Count
                FROM Food f
                WHERE f.Id = @Id";

            command.Parameters.AddWithValue("Id", id);

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Food food = new Food(
                    id: reader.GetInt64("Id"),
                    animalType: (AnimalType)reader.GetInt64("AnimalType"),
                    name: reader.GetString("Name"),
                    count: reader.GetInt32("Count"));

                return food;
            }

            return null;
        }

        private static async Task<IEnumerable<Feeding>> GetFeedingsAsync(SQLiteConnection connection, long animalId)
        {
            ISet<Feeding> feedings = new HashSet<Feeding>();

            await using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    f.Id,
                    f.DateTimeUtc,
                    f.FoodId,
                    f.Count
                FROM Feeding f
                WHERE f.AnimalId = @AnimalId";

            command.Parameters.AddWithValue("AnimalId", animalId);

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            IDictionary<long, Food> foodCache = new Dictionary<long, Food>();

            while (await reader.ReadAsync())
            {
                long foodId = reader.GetInt64("FoodId");

                if (!foodCache.ContainsKey(foodId))
                {
                    Food food = await GetFoodAsync(connection, foodId);

                    foodCache.Add(foodId, food);
                }

                Feeding feeding = feeding = new Feeding(
                    id: reader.GetInt64("Id"),
                    dateTimeUtc: DateTime.SpecifyKind(
                        DateTime.ParseExact(
                            reader.GetString("DateTimeUtc"),
                            DateTimeFormat,
                            CultureInfo.InvariantCulture),
                        DateTimeKind.Utc),
                    food: foodCache[foodId],
                    count: reader.GetInt32("Count"));

                feedings.Add(feeding);
            }

            return feedings.AsEnumerable();
        }

        private static async Task<Animal> GetAnimalAsync(SQLiteConnection connection, long id)
        {
            Animal animal = null;

            await using SQLiteCommand command = connection.CreateCommand();

            command.CommandText = @"
                SELECT
                    a.Id,
                    a.Type,
                    a.Name ,
                    a.BreedId,
                    c.Weight Weight,
                    d.TailLength TailLength
                FROM Animal a
                LEFT JOIN Cat c ON c.AnimalId = a.Id
                LEFT JOIN Dog d ON d.AnimalId = a.Id
                WHERE a.Id = @Id";

            command.Parameters.AddWithValue("Id", id);

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                string name = reader.GetString("Name");
                AnimalType type = (AnimalType)reader.GetInt64("Type");
                long breedId = reader.GetInt64("BreedId");

                Breed breed = await GetBreedAsync(connection, breedId);

                IEnumerable<Feeding> feedings = await GetFeedingsAsync(connection, id);

                switch (type)
                {
                    case AnimalType.Cat:
                        animal = new Cat(
                            id: id,
                            name: name,
                            breed: breed,
                            weight: reader.GetDecimal("Weight"),
                            feedings: feedings);
                        break;

                    case AnimalType.Dog:
                        animal = new Dog(
                            id: id,
                            name: name,
                            breed: breed,
                            tailLength: reader.GetDecimal("TailLength"),
                            feedings: feedings);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown animal type");
                }
            }

            return animal;
        }

    }
}