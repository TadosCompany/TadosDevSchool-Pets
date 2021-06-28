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


    public abstract class LinqAsyncQueryBase<TCriterion, TResult> : IAsyncQuery<TCriterion, TResult>
        where TCriterion : ICriterion
    {
        private readonly ILinqProvider _linqProvider;
        private readonly IAsyncQueryableFactory _asyncQueryableFactory;


        protected LinqAsyncQueryBase(ILinqProvider linqProvider, IAsyncQueryableFactory asyncQueryableFactory)
        {
            _linqProvider = linqProvider ?? throw new ArgumentNullException(nameof(linqProvider));
            _asyncQueryableFactory =
                asyncQueryableFactory ?? throw new ArgumentNullException(nameof(asyncQueryableFactory));
        }

        protected virtual IQueryable<THasId> Query<THasId>()
            where THasId : class, IHasId, new()
            => _linqProvider.Query<THasId>();
        
        protected virtual IAsyncQueryable<THasId> AsyncQuery<THasId>()
            where THasId : class, IHasId, new()
            => ToAsync(Query<THasId>());
        
        protected IAsyncQueryable<THasId> ToAsync<THasId>(IQueryable<THasId> query)
            where THasId : class, IHasId, new()
            => _asyncQueryableFactory.CreateFrom(query);

        public abstract Task<TResult> AskAsync(TCriterion criterion, CancellationToken cancellationToken = default);
    }
}