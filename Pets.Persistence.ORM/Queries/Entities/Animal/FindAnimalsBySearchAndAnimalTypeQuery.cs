namespace Pets.Persistence.ORM.Queries.Entities.Animal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Criteria;
    using Domain.Entities;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;


    public class FindAnimalsBySearchAndAnimalTypeQuery :
        LinqAsyncQueryBase<Animal, FindBySearchAndAnimalType, List<Animal>>
    {
        public FindAnimalsBySearchAndAnimalTypeQuery(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory) : base(linqProvider, asyncQueryableFactory)
        {
        }


        public override Task<List<Animal>> AskAsync(
            FindBySearchAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Animal> query = Query;

            if (!string.IsNullOrWhiteSpace(criterion.Search))
                query = query.Where(x => x.Name.Contains(criterion.Search));

            if (criterion.AnimalType.HasValue)
                query = query.Where(x => x.Type == criterion.AnimalType.Value);

            query = query.OrderBy(x => x.Name);

            return ToAsync(query).ToListAsync(cancellationToken);
        }
    }
}