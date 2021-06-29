namespace Pets.Persistence.ORM.Queries
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Domain.Abstractions;
    using global::Queries.Abstractions;
    using Linq.AsyncQueryable.Abstractions;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;


    public abstract class LinqAsyncQueryBase<THasId, TCriterion, TResult> : IAsyncQuery<TCriterion, TResult>
        where THasId : class, IHasId, new()
        where TCriterion : ICriterion
    {
        private readonly ILinqProvider _linqProvider;
        private readonly IAsyncQueryableFactory _asyncQueryableFactory;


        protected LinqAsyncQueryBase(
            ILinqProvider linqProvider,
            IAsyncQueryableFactory asyncQueryableFactory)
        {
            _linqProvider = linqProvider ?? throw new ArgumentNullException(nameof(linqProvider));
            _asyncQueryableFactory =
                asyncQueryableFactory ?? throw new ArgumentNullException(nameof(asyncQueryableFactory));
        }

        protected virtual IQueryable<THasId> Query
            => _linqProvider.Query<THasId>();

        protected virtual IAsyncQueryable<THasId> AsyncQuery()
            => ToAsync(Query);

        protected IAsyncQueryable<THasId> ToAsync(IQueryable<THasId> query)
            => _asyncQueryableFactory.CreateFrom(query);


        public abstract Task<TResult> AskAsync(TCriterion criterion, CancellationToken cancellationToken = default);
    }
}