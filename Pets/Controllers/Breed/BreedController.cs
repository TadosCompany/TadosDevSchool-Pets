namespace Pets.Controllers.Breed
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using Add;
    using Get;
    using GetList;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Providers;


    [ApiController]
    [Route("api/breed")]
    public class BreedController : Controller
    {
        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(BreedGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList(BreedGetListRequest request)
        {
            List<Breed> breeds = new List<Breed>();

            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction =
                await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand command = connection.CreateCommand();

            List<string> conditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                conditions.Add("b.Name LIKE '%' || @Search || '%'");
                command.Parameters.AddWithValue("Search", request.Search);
            }

            if (request.AnimalType.HasValue)
            {
                conditions.Add("b.AnimalType = @AnimalType");
                command.Parameters.AddWithValue("AnimalType", (int)request.AnimalType.Value);
            }

            command.CommandText = @$"
                SELECT
                    b.Id,
                    b.Name,
                    b.AnimalType
                FROM Breed b
                {(conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty)}
                ORDER BY b.Name";

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Breed breed = CreateFromReader(reader);

                breeds.Add(breed);
            }

            await transaction.CommitAsync();

            BreedGetListResponse response = new BreedGetListResponse()
            {
                Breeds = breeds,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(BreedGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(BreedGetRequest request)
        {
            Breed breed = null;

            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    b.Id,
                    b.Name,
                    b.AnimalType
                FROM Breed b
                WHERE b.Id = @Id";

            command.Parameters.AddWithValue("Id", request.Id);

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                breed = CreateFromReader(reader);
            }

            await transaction.CommitAsync();

            BreedGetResponse response = new BreedGetResponse()
            {
                Breed = breed,
            };
            
            return Json(response);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(BreedAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(BreedAddRequest request)
        {
            Breed breed = new Breed()
            {
                Name = request.Name.Trim(),
                AnimalType = request.AnimalType
            };
            
            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand checkCommand = connection.CreateCommand();
            checkCommand.CommandText = @"
                SELECT
                    COUNT(1)
                FROM Breed b
                WHERE b.Name = @Name AND b.AnimalType = @AnimalType";
            
            checkCommand.Parameters.AddWithValue("Name", breed.Name);
            checkCommand.Parameters.AddWithValue("AnimalType", (int)breed.AnimalType);

            long existingCount = (long)await checkCommand.ExecuteScalarAsync();

            if (existingCount > 0)
                throw new Exception($"Breed with name {breed.Name} for {breed.AnimalType} already exists");

            await using SQLiteCommand insertCommand = connection.CreateCommand();
            insertCommand.CommandText = @"
                INSERT INTO Breed
                (
                    Name,
                    AnimalType    
                )
                VALUES
                (
                    @Name,
                    @AnimalType
                ); SELECT last_insert_rowid();";
            
            insertCommand.Parameters.AddWithValue("Name", breed.Name);
            insertCommand.Parameters.AddWithValue("AnimalType", (int)breed.AnimalType);

            breed.Id = (long)(await insertCommand.ExecuteScalarAsync());

            await transaction.CommitAsync();

            BreedAddResponse response = new BreedAddResponse()
            {
                Id = breed.Id,
            };
            
            return Json(response);
        }

        private static Breed CreateFromReader(DbDataReader reader)
        {
            Breed breed = new Breed()
            {
                Id = reader.GetInt64("Id"),
                Name = reader.GetString("Name"),
                AnimalType = (AnimalType)reader.GetInt64("AnimalType"),
            };

            return breed;
        }
    }
}