namespace Pets.Persistence.ORM.Queries.Entities.FeedLimit
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Criteria;
    using Domain.Entities;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;

    public class FindFeedLimitsBySearchAndAnimalTypeQuery :
        LinqAsyncQueryBase<FeedLimit, FindBySearchAndAnimalType, List<FeedLimit>>
    {
        public FindFeedLimitsBySearchAndAnimalTypeQuery(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory) : base(linqProvider, asyncQueryableFactory)
        {
        }



        public override Task<List<FeedLimit>> AskAsync(
            FindBySearchAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            IQueryable<FeedLimit> query = Query;
            
            if (!string.IsNullOrWhiteSpace(criterion.Search))
                query = query.Where(x => x.Breed.Name.Contains(criterion.Search));

            if (criterion.AnimalType.HasValue)
                query = query.Where(x => x.Breed.AnimalType == criterion.AnimalType.Value);

            query = query.OrderBy(x => x.Breed.AnimalType).ThenBy(x => x.Breed.Name);
            
            return ToAsync(query).ToListAsync(cancellationToken);
        }
    }
}