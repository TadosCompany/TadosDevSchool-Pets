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
    using Models;
    using Providers;

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

            await using DbTransaction transaction =
                await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

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
                    a.Id AId,
                    a.Type AType,
                    a.Name AName,
                    b.Id BId,
                    b.AnimalType BAnimalType,
                    b.Name BName,
                    c.Weight CWeight,
                    d.TailLength DTailLength,
                    f2.Id F2Id,
                    f2.AnimalType F2AnimalType,
                    f2.Name F2Name,
                    f2.Count F2Count,
                    f1.Id F1Id,
                    f1.DateTimeUtc F1DateTimeUtc,
                    f1.Count F1Count
                FROM Animal a
                JOIN Breed b ON b.Id = a.BreedId
                LEFT JOIN Cat c ON c.AnimalId = a.Id
                LEFT JOIN Dog d ON d.AnimalId = a.Id
                LEFT JOIN Feeding f1 ON f1.AnimalId = a.Id
                LEFT JOIN Food f2 ON f1.FoodId = f2.Id
                {(conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty)}
                ORDER BY a.Name, f1.DateTimeUtc DESC";

            Dictionary<long, Food> foodsMap = new Dictionary<long, Food>();
            Dictionary<long, Breed> breedsMap = new Dictionary<long, Breed>();
            Dictionary<long, Animal> animalsMap = new Dictionary<long, Animal>();
            Dictionary<long, List<Feeding>> animalsFeedingMap = new Dictionary<long, List<Feeding>>();

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                long animalId = reader.GetInt64("AId");

                if (!animalsMap.ContainsKey(animalId))
                {
                    long breedId = reader.GetInt64("BId");

                    if (!breedsMap.ContainsKey(breedId))
                    {
                        breedsMap.Add(breedId, CreateBreedFromReader(reader));
                    }

                    Breed breed = breedsMap[breedId];
                    Animal animal = CreateAnimalFromReader(reader, breed);

                    animalsMap.Add(animalId, animal);
                    List<Feeding> feedings = new List<Feeding>();

                    animalsFeedingMap.Add(animalId, feedings);

                    animal.Feedings = feedings;
                    animals.Add(animal);
                }

                object f2Id = reader["F2Id"];

                if (f2Id != DBNull.Value)
                {
                    long foodId = (long)f2Id;

                    if (!foodsMap.ContainsKey(foodId))
                    {
                        foodsMap.Add(foodId, CreateFoodFromReader(reader));
                    }

                    Food food = foodsMap[foodId];
                    Feeding feeding = CreateFeedingFromReader(reader, food);

                    animalsFeedingMap[animalId].Add(feeding);
                }
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
            Animal animal = null;
            List<Feeding> feedings = null;

            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction =
                await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand command = connection.CreateCommand();

            command.CommandText = @"
                SELECT
                    a.Id AId,
                    a.Type AType,
                    a.Name AName,
                    b.Id BId,
                    b.AnimalType BAnimalType,
                    b.Name BName,
                    c.Weight CWeight,
                    d.TailLength DTailLength,
                    f2.Id F2Id,
                    f2.AnimalType F2AnimalType,
                    f2.Name F2Name,
                    f2.Count F2Count,
                    f1.Id F1Id,
                    f1.DateTimeUtc F1DateTimeUtc,
                    f1.Count F1Count
                FROM Animal a
                JOIN Breed b ON b.Id = a.BreedId
                LEFT JOIN Cat c ON c.AnimalId = a.Id
                LEFT JOIN Dog d ON d.AnimalId = a.Id
                LEFT JOIN Feeding f1 ON f1.AnimalId = a.Id
                LEFT JOIN Food f2 ON f1.FoodId = f2.Id
                WHERE a.Id = @Id
                ORDER BY f1.DateTimeUtc DESC";

            command.Parameters.AddWithValue("Id", request.Id);

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            Dictionary<long, Food> foodsMap = new Dictionary<long, Food>();

            while (await reader.ReadAsync())
            {
                if (animal == null)
                {
                    Breed breed = CreateBreedFromReader(reader);
                    animal = CreateAnimalFromReader(reader, breed);
                    feedings = new List<Feeding>();
                    animal.Feedings = feedings;
                }

                object f2Id = reader["F2Id"];

                if (f2Id != DBNull.Value)
                {
                    long foodId = (long)f2Id;

                    if (!foodsMap.ContainsKey(foodId))
                    {
                        foodsMap.Add(foodId, CreateFoodFromReader(reader));
                    }

                    Food food = foodsMap[foodId];
                    Feeding feeding = CreateFeedingFromReader(reader, food);

                    feedings.Add(feeding);
                }
            }

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

            await using SQLiteCommand checkAnimalCommand = connection.CreateCommand();

            request.Name = request.Name?.Trim();

            checkAnimalCommand.CommandText = @"
                SELECT
                    COUNT(1)
                FROM Animal a
                WHERE a.Name = @Name;";

            checkAnimalCommand.Parameters.AddWithValue("Name", request.Name);

            long currentCount = (long)checkAnimalCommand.ExecuteScalar();

            if (currentCount > 0)
                throw new Exception($"Animal with name {request.Name} already exists");

            await using SQLiteCommand checkBreedCommand = connection.CreateCommand();

            checkBreedCommand.CommandText = @"
                SELECT
                    COUNT(1)
                FROM Breed b
                WHERE b.Id = @BreedId AND b.AnimalType = @Type";
            checkBreedCommand.Parameters.AddWithValue("BreedId", request.BreedId);
            checkBreedCommand.Parameters.AddWithValue("Type", request.Type);
            
            long breedsCount = (long)checkBreedCommand.ExecuteScalar();
            
            if (breedsCount < 1)
                throw new Exception($"Breed with Id {request.BreedId} not found for {request.Type}");
            
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

            insertAnimalCommand.Parameters.AddWithValue("Type", (int)request.Type);
            insertAnimalCommand.Parameters.AddWithValue("Name", request.Name);
            insertAnimalCommand.Parameters.AddWithValue("BreedId", request.BreedId);

            long animalId = (long)insertAnimalCommand.ExecuteScalar();

            switch (request.Type)
            {
                case AnimalType.Cat:
                    decimal weight = request.Weight ?? throw new ArgumentNullException(nameof(request.Weight));

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
                        insertCatCommand.Parameters.AddWithValue("AnimalId", animalId);
                        insertCatCommand.Parameters.AddWithValue("Weight", weight);

                        insertCatCommand.ExecuteNonQuery();
                    }

                    break;
                case AnimalType.Dog:
                    decimal tailLength =
                        request.TailLength ?? throw new ArgumentNullException(nameof(request.TailLength));

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
                        insertDogCommand.Parameters.AddWithValue("AnimalId", animalId);
                        insertDogCommand.Parameters.AddWithValue("TailLength", tailLength);

                        await insertDogCommand.ExecuteNonQueryAsync();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(request.Type), request.Type, "Unknown animal type");
            }

            await transaction.CommitAsync();

            AnimalAddResponse response = new AnimalAddResponse()
            {
                Id = animalId,
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

            await using DbTransaction transaction =
                await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand getAnimalTypeCommand = connection.CreateCommand();
            
            getAnimalTypeCommand.CommandText = @"SELECT a.Type FROM Animal a WHERE a.Id = @AnimalId";
            getAnimalTypeCommand.Parameters.AddWithValue("AnimalId", request.AnimalId);

            await using DbDataReader animalTypeReader = getAnimalTypeCommand.ExecuteReader();

            if (!await animalTypeReader.ReadAsync())
                throw new Exception($"Animal with id {request.AnimalId} not found");

            AnimalType animalType = (AnimalType)animalTypeReader.GetInt64("Type");
            
            await using SQLiteCommand checkFoodCommand = connection.CreateCommand();

            checkFoodCommand.CommandText = @"
                SELECT
                    COUNT(1)
                FROM Food f WHERE f.Id = @FoodId AND f.AnimalType = @AnimalType AND f.Count >= @Count";
            checkFoodCommand.Parameters.AddWithValue("FoodId", request.FoodId);
            checkFoodCommand.Parameters.AddWithValue("AnimalType", (int)animalType);
            checkFoodCommand.Parameters.AddWithValue("Count", request.Count);

            bool checkFoodResult = (long)await checkFoodCommand.ExecuteScalarAsync() > 0;

            if (!checkFoodResult)
                throw new Exception(
                    $"Food for {animalType} with Id {request.FoodId} and count {request.Count} not found");

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

            insertFeedingCommand.Parameters.AddWithValue("AnimalId", request.AnimalId);
            insertFeedingCommand.Parameters.AddWithValue("FoodId", request.FoodId);
            insertFeedingCommand.Parameters.AddWithValue("DateTimeUtc", DateTime.UtcNow.ToString(DateTimeFormat));
            insertFeedingCommand.Parameters.AddWithValue("Count", request.Count);

            await insertFeedingCommand.ExecuteNonQueryAsync();

            await using SQLiteCommand updateFoodCommand = connection.CreateCommand();

            updateFoodCommand.CommandText = @"UPDATE Food SET Count = Count - @Count WHERE Id = @FoodId";
            updateFoodCommand.Parameters.AddWithValue("FoodId", request.FoodId);
            updateFoodCommand.Parameters.AddWithValue("Count", request.Count);

            await updateFoodCommand.ExecuteNonQueryAsync();
            
            await transaction.CommitAsync();

            AnimalFeedResponse response = new AnimalFeedResponse();

            return Json(response);
        }

        private static Breed CreateBreedFromReader(DbDataReader reader)
        {
            Breed breed = new Breed()
            {
                Id = reader.GetInt64("BId"),
                AnimalType = (AnimalType)reader.GetInt32("BAnimalType"),
                Name = reader.GetString("BName"),
            };

            return breed;
        }

        private static Animal CreateAnimalFromReader(DbDataReader reader, Breed breed)
        {
            long id = reader.GetInt64("AId");
            string name = reader.GetString("AName");
            AnimalType type = (AnimalType)reader.GetInt64("AType");

            switch (type)
            {
                case AnimalType.Cat:
                    return new Cat()
                    {
                        Id = id,
                        Name = name,
                        Type = type,
                        Breed = breed,
                        Weight = reader.GetDecimal("CWeight"),
                    };

                case AnimalType.Dog:
                    return new Dog()
                    {
                        Id = id,
                        Name = name,
                        Type = type,
                        Breed = breed,
                        TailLength = reader.GetDecimal("DTailLength")
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown animal type");
            }
        }

        private static Food CreateFoodFromReader(DbDataReader reader)
        {
            Food food = new Food()
            {
                Id = reader.GetInt64("F2Id"),
                AnimalType = (AnimalType)reader.GetInt32("F2AnimalType"),
                Name = reader.GetString("F2Name"),
                Count = reader.GetInt32("F2Count")
            };

            return food;
        }

        private static Feeding CreateFeedingFromReader(DbDataReader reader, Food food)
        {
            Feeding feeding = new Feeding()
            {
                Id = reader.GetInt64("F1Id"),
                Food = food,
                DateTimeUtc = DateTime.SpecifyKind(
                    DateTime.ParseExact(reader.GetString("F1DateTimeUtc"), DateTimeFormat, CultureInfo.InvariantCulture),
                    DateTimeKind.Utc),
                Count = reader.GetInt32("F1Count"),
            };

            return feeding;
        }
    }
}