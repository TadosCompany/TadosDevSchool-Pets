namespace Pets.Domain.Services.Foods
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.Contexts;
    using Criteria;
    using Entities;
    using Enums;
    using Exceptions;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class FoodService : IFoodService
    {
        private readonly IAsyncQueryBuilder _queryBuilder;
        private readonly IAsyncCommandBuilder _commandBuilder;


        public FoodService(IAsyncQueryBuilder queryBuilder, IAsyncCommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            _commandBuilder = commandBuilder ?? throw new ArgumentNullException(nameof(commandBuilder));
        }


        public async Task<Food> CreateFoodAsync(AnimalType animalType, string name, CancellationToken cancellationToken = default)
        {
            await CheckIsFoodWithNameExistAsync(animalType, name, cancellationToken);

            Food food = new Food(animalType, name);

            await _commandBuilder.ExecuteAsync(new CreateFoodCommandContext(food), cancellationToken);
            
            return food;
        }


        private async Task CheckIsFoodWithNameExistAsync(AnimalType animalType, string name, CancellationToken cancellationToken = default)
        {
            int existingCount = await _queryBuilder
                .For<int>()
                .WithAsync(new FindFoodsCountByNameAndAnimalType(name, animalType), cancellationToken);
            
            if (existingCount != 0)
                throw new NameAlreadyExistsException();
            
            // await using SQLiteCommand command = _connection.CreateCommand();
            // command.CommandText = @"
            //     SELECT
            //         COUNT(1)
            //     FROM Food f
            //     WHERE f.Name = @Name AND f.AnimalType = @AnimalType";
            //
            // command.Parameters.AddWithValue("Name", name);
            // command.Parameters.AddWithValue("AnimalType", (int)animalType);
            //
            // var existingCount = (long)await command.ExecuteScalarAsync();
        }
    }
}