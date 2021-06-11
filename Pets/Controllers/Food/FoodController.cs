namespace Pets.Controllers.Food
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using Add;
    using Append;
    using Get;
    using GetList;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Providers;


    [ApiController]
    [Route("api/food")]
    public class FoodController : Controller
    {
        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(FoodGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList(FoodGetListRequest request)
        {
            List<Food> foods = new List<Food>();
            
            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand command = connection.CreateCommand();
            
            List<string> conditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                conditions.Add("f.Name LIKE '%' || @Search || '%'");
                command.Parameters.AddWithValue("Search", request.Search);
            }

            if (request.AnimalType.HasValue)
            {
                conditions.Add("f.AnimalType = @AnimalType");
                command.Parameters.AddWithValue("AnimalType", (int)request.AnimalType.Value);
            }

            command.CommandText = @$"
                SELECT
                    f.Id,
                    f.Name,
                    f.AnimalType,
                    f.Count
                FROM Food f
                {(conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty)}
                ORDER BY f.Name";

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Food food = CreateFromReader(reader);
                
                foods.Add(food);
            }

            await transaction.CommitAsync();

            FoodGetListResponse response = new FoodGetListResponse()
            {
                Foods = foods,
            };
            
            return Json(response);
        }

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(FoodGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(FoodGetRequest request)
        {
            Food food = null;

            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    f.Id,
                    f.Name,
                    f.AnimalType,
                    f.Count
                FROM Food f
                WHERE f.Id = @Id";

            command.Parameters.AddWithValue("Id", request.Id);

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                food = CreateFromReader(reader);
            }

            await transaction.CommitAsync();

            FoodGetResponse response = new FoodGetResponse()
            {
                Food = food,
            };
            
            return Json(response);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(FoodAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(FoodAddRequest request)
        {
            Food food = new Food()
            {
                Count = 0,
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
                FROM Food f
                WHERE f.Name = @Name AND f.AnimalType = @AnimalType";
            
            checkCommand.Parameters.AddWithValue("Name", food.Name);
            checkCommand.Parameters.AddWithValue("AnimalType", food.AnimalType);

            long existingCount = (long)await checkCommand.ExecuteScalarAsync();

            if (existingCount > 0)
                throw new Exception($"Food with name {food.Name} for {food.AnimalType} already exists");

            await using SQLiteCommand insertCommand = connection.CreateCommand();
            insertCommand.CommandText = @"
                INSERT INTO Food
                (
                    Name,
                    AnimalType,
                    Count    
                )
                VALUES
                (
                    @Name,
                    @AnimalType,
                    @Count
                ); SELECT last_insert_rowid();";
            
            insertCommand.Parameters.AddWithValue("Name", food.Name);
            insertCommand.Parameters.AddWithValue("AnimalType", food.AnimalType);
            insertCommand.Parameters.AddWithValue("Count", food.Count);

            food.Id = (long)(await insertCommand.ExecuteScalarAsync());

            await transaction.CommitAsync();

            FoodAddResponse response = new FoodAddResponse()
            {
                Id = food.Id,
            };
            
            return Json(response);
        }

        [HttpPost]
        [Route("append")]
        [ProducesResponseType(typeof(FoodAppendResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Append(FoodAppendRequest request)
        {
            await using SQLiteConnection connection = new SQLiteConnection(DatabaseProvider.ConnectionString);
            await connection.OpenAsync();

            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Food
                SET
                    Count = Count + @Count
                WHERE
                    Food.Id = @Id";

            command.Parameters.AddWithValue("Id", request.Id);
            command.Parameters.AddWithValue("Count", request.Count);

            await command.ExecuteNonQueryAsync();
            
            await transaction.CommitAsync();

            FoodAppendResponse response = new FoodAppendResponse();
            
            return Json(response);
        }

        private static Food CreateFromReader(DbDataReader reader)
        {
            Food food = new Food()
            {
                Id = reader.GetInt64("Id"),
                Name = reader.GetString("Name"),
                AnimalType = (AnimalType)reader.GetInt64("AnimalType"),
                Count = reader.GetInt32("Count"),
            };

            return food;
        }
    }
}