namespace Pets.Persistence.ORM.Queries.Entities.Food
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Criteria;
    using Domain.Entities;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;


    public class FindFoodsBySearchAndAnimalTypeQuery :
        LinqAsyncQueryBase<Food, FindBySearchAndAnimalType, List<Food>>
    {
        public FindFoodsBySearchAndAnimalTypeQuery(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory) : base(linqProvider, asyncQueryableFactory)
        {
        }

        public override Task<List<Food>> AskAsync(
            FindBySearchAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Food> query = Query;

            if (!string.IsNullOrWhiteSpace(criterion.Search))
                query = query.Where(x => x.Name.Contains(criterion.Search));

            if (criterion.AnimalType.HasValue)
                query = query.Where(x => x.AnimalType == criterion.AnimalType.Value);

            query = query.OrderBy(x => x.Name);

            return ToAsync(query).ToListAsync(cancellationToken);
        }
    }
}