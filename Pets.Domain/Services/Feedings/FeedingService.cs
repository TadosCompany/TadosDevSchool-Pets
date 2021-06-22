namespace Pets.Domain.Services.Feedings
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.Contexts;
    using Entities;
    using Exceptions;
    using global::Commands.Abstractions;
    using ValueObjects;

    public class FeedingService : IFeedingService
    {
        private readonly IAsyncCommandBuilder _commandBuilder;


        public FeedingService(IAsyncCommandBuilder commandBuilder)
        {
            _commandBuilder = commandBuilder ?? throw new ArgumentNullException(nameof(commandBuilder));
        }


        public async Task FeedAsync(Animal animal, Food food, int count, CancellationToken cancellationToken = default)
        {
            if (animal == null)
                throw new ArgumentNullException(nameof(animal));

            if (food == null)
                throw new ArgumentNullException(nameof(food));

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (food.Count < count)
                throw new NotEnoughFoodException();

            if (food.AnimalType != animal.Type)
                throw new InvalidOperationException("Selected food can't be used for animal");

            Feeding feeding = animal.Feed(food, count);
            food.Decrease(count);

            await _commandBuilder.ExecuteAsync(new UpdateFoodCommandContext(food), cancellationToken);
            await _commandBuilder.ExecuteAsync(new CreateFeedingCommandContext(animal, feeding), cancellationToken);
        }
    }
}