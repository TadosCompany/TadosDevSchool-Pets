namespace Pets.Domain.Services.Feedings
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Criteria;
    using Entities;
    using Exceptions;
    using Queries.Abstractions;

    public class FeedingService : IFeedingService
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;



        public FeedingService(IAsyncQueryBuilder asyncQueryBuilder)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
        }



        public async Task FeedAsync(
            Animal animal, 
            Food food, 
            int count,
            CancellationToken cancellationToken = default)
        {
            if (animal == null)
                throw new ArgumentNullException(nameof(animal));

            if (food == null)
                throw new ArgumentNullException(nameof(food));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (food.Count < count)
                throw new NotEnoughFoodException();

            FeedLimit feedLimit = await _asyncQueryBuilder
                .For<FeedLimit>()
                .WithAsync(new FindByBreed(animal.Breed), cancellationToken);

            if (animal.Feedings.Count() >= feedLimit?.MaxPerDay)
                throw new FeedLimitExceededException();

            animal.Feed(food, count);
            food.Decrease(count);
        }
    }
}
