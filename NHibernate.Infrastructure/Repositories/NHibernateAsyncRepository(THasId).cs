namespace NHibernate.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Abstractions;
    using global::Repositories.Abstractions;
    using NHibernate.Linq;
    using Sessions.Abstractions;


    public class NHibernateAsyncRepository<THasId> : IAsyncRepository<THasId>
        where THasId : class, IEntity, new()
    {
        private readonly ISessionProvider _sessionProvider;


        public NHibernateAsyncRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider ?? throw new ArgumentNullException(nameof(sessionProvider));
        }

        private ISession Session => _sessionProvider.CurrentSession;
        

        public Task AddAsync(THasId objectWithId, CancellationToken cancellationToken = default)
        {
            return Session.SaveOrUpdateAsync(objectWithId, cancellationToken);
        }

        public Task DeleteAsync(THasId objectWithId, CancellationToken cancellationToken = default)
        {
            return Session.DeleteAsync(objectWithId, cancellationToken);
        }

        public Task UpdateAsync(THasId objectWithId, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<THasId> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return Session.Query<THasId>().SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
        }

        public Task<List<THasId>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Session.Query<THasId>().ToListAsync(cancellationToken);
        }
    }
}