namespace Pets.Domain.Services
{
    using System;
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using global::Domain.Abstractions;
    using Entities;
    using Enums;
    using Exceptions;

    public class BreedService : IDomainService
    {
        private readonly SQLiteConnection _connection;



        public BreedService(SQLiteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }



        public async Task<Breed> CreateBreedAsync(AnimalType animalType, string name)
        {
            await CheckIsBreedWithNameExistAsync(animalType, name);

            return new Breed(animalType, name);
        }



        private async Task CheckIsBreedWithNameExistAsync(AnimalType animalType, string name)
        {
            await using SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    COUNT(1)
                FROM Breed b
                WHERE b.Name = @Name AND b.AnimalType = @AnimalType";

            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("AnimalType", (int)animalType);

            var existingCount = (long)await command.ExecuteScalarAsync();

            if (existingCount != 0)
                throw new NameAlreadyExistsException();
        }
    }
}
