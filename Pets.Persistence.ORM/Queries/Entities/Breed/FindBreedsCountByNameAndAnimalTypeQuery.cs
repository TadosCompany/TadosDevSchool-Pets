namespace Pets.Persistence.ORM.Queries.Entities.Breed
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Criteria;
    using Domain.Entities;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;


    public class FindBreedsCountByNameAndAnimalTypeQuery :
        LinqAsyncQueryBase<Breed, FindBreedsCountByNameAndAnimalType, int>
    {
        public FindBreedsCountByNameAndAnimalTypeQuery(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory) : base(linqProvider, asyncQueryableFactory)
        {
        }


        public override Task<int> AskAsync(FindBreedsCountByNameAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            return AsyncQuery().CountAsync(
                x => x.Name == criterion.Name && x.AnimalType == criterion.AnimalType,
                cancellationToken);
        }
    }
}