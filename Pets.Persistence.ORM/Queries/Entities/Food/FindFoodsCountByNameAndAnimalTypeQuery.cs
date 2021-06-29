namespace Pets.Persistence.ORM.Queries.Entities.Food
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Criteria;
    using Domain.Entities;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;


    public class FindFoodsCountByNameAndAnimalTypeQuery :
        LinqAsyncQueryBase<Food, FindFoodsCountByNameAndAnimalType, int>
    {
        public FindFoodsCountByNameAndAnimalTypeQuery(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory) : base(linqProvider, asyncQueryableFactory)
        {
        }


        public override Task<int> AskAsync(
            FindFoodsCountByNameAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            return AsyncQuery().CountAsync(
                x => x.Name == criterion.Name && x.AnimalType == criterion.AnimalType,
                cancellationToken);
        }
    }
}