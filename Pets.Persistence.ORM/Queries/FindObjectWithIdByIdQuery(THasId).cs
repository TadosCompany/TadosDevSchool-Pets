namespace Pets.Persistence.ORM.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Criteria;
    using global::Domain.Abstractions;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;


    public class FindObjectWithIdByIdQuery<THasId> : LinqAsyncQueryBase<THasId, FindById, THasId>
        where THasId : class, IHasId, new()
    {
        public FindObjectWithIdByIdQuery(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory) : base(linqProvider, asyncQueryableFactory)
        {
        }

        public override Task<THasId> AskAsync(FindById criterion, CancellationToken cancellationToken = default)
        {
            return AsyncQuery().SingleOrDefaultAsync(x => x.Id == criterion.Id, cancellationToken);
        }
    }
}