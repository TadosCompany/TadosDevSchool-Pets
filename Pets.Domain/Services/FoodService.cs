namespace Pets.Domain.Services
{
    using System;
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using global::Domain.Abstractions;
    using Entities;
    using Enums;
    using Exceptions;

    public class FoodService : IDomainService
    {
        private readonly SQLiteConnection _connection;



        public FoodService(SQLiteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }



        public async Task<Food> CreateFoodAsync(AnimalType animalType, string name)
        {
            await CheckIsFoodWithNameExistAsync(animalType, name);

            return new Food(animalType, name);
        }



        private async Task CheckIsFoodWithNameExistAsync(AnimalType animalType, string name)
        {
            await using SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    COUNT(1)
                FROM Food f
                WHERE f.Name = @Name AND f.AnimalType = @AnimalType";

            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("AnimalType", (int)animalType);

            var existingCount = (long)await command.ExecuteScalarAsync();

            if (existingCount != 0)
                throw new NameAlreadyExistsException();
        }
    }
}
