namespace Pets.Persistence.ORM.Queries.Entities.Animal
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Criteria;
    using Domain.Entities;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;

    public class FindAnimalsCountByNameAndAnimalTypeQuery :
        LinqAsyncQueryBase<Animal, FindAnimalsCountByNameAndAnimalType, int>
    {
        public FindAnimalsCountByNameAndAnimalTypeQuery(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory) : base(linqProvider, asyncQueryableFactory)
        {
        }


        public override Task<int> AskAsync(FindAnimalsCountByNameAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            return AsyncQuery().CountAsync(
                x => x.Name == criterion.Name && x.Type == criterion.AnimalType,
                cancellationToken);
        }
    }
}