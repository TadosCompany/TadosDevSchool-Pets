namespace Pets.Domain.Services.FeedLimits
{
    using System;
    using System.Threading.Tasks;
    using Commands.Contexts;
    using Criteria;
    using Entities;
    using Exceptions;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class FeedLimitService : IFeedLimitService
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IAsyncCommandBuilder _asyncCommandBuilder;



        public FeedLimitService(IAsyncQueryBuilder asyncQueryBuilder, IAsyncCommandBuilder asyncCommandBuilder)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _asyncCommandBuilder = asyncCommandBuilder ?? throw new ArgumentNullException(nameof(asyncCommandBuilder));
        }



        public async Task<FeedLimit> CreateFeedLimitAsync(Breed breed, int maxPerDay)
        {
            FeedLimit feedLimit = await _asyncQueryBuilder
                .For<FeedLimit>()
                .WithAsync(new FindByBreed(breed));

            if (feedLimit != null)
                throw new FeedLimitForBreedAlreadyExistException();

            feedLimit = new FeedLimit(breed, maxPerDay);

            await _asyncCommandBuilder.CreateAsync(feedLimit);

            return feedLimit;
        }
    }
}
