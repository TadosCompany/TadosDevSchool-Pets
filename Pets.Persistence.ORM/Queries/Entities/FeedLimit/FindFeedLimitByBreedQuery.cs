namespace Pets.Persistence.ORM.Queries.Entities.FeedLimit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Criteria;
    using Domain.Entities;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;

    public class FindFeedLimitByBreedQuery :
        LinqAsyncQueryBase<FeedLimit, FindByBreed, FeedLimit>
    {
        public FindFeedLimitByBreedQuery(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory) : base(linqProvider, asyncQueryableFactory)
        {
        }



        public override Task<FeedLimit> AskAsync(
            FindByBreed criterion,
            CancellationToken cancellationToken = default)
        {
            return AsyncQuery().SingleOrDefaultAsync(x => x.Breed == criterion.Breed, cancellationToken);
        }
    }
}