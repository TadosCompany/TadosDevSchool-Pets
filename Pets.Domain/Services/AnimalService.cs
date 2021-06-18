namespace Pets.Domain.Services
{
    using System;
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using global::Domain.Abstractions;
    using Enums;
    using Exceptions;

    public class AnimalService : IDomainService
    {
        private readonly SQLiteConnection _connection;



        protected AnimalService(SQLiteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }



        protected async Task CheckIsAnimalWithNameExistAsync(AnimalType animalType, string name)
        {
            await using SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    COUNT(1)
                FROM Animal a
                WHERE a.Name = @Name AND a.Type = @AnimalType";

            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("AnimalType", (int)animalType);

            var existingCount = (long)await command.ExecuteScalarAsync();

            if (existingCount != 0)
                throw new NameAlreadyExistsException();
        }
    }
}
