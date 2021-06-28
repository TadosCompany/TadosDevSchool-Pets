namespace NHibernate.Infrastructure.Linq.Providers
{
    using System;
    using System.Linq;
    using Domain.Abstractions;
    using global::Linq.Providers.Abstractions;
    using Sessions.Abstractions;


    public class NHibernateLinqProvider : ILinqProvider
    {
        private readonly ISessionProvider _sessionProvider;


        public NHibernateLinqProvider(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider ?? throw new ArgumentNullException(nameof(sessionProvider));
        }


        public IQueryable<THasId> Query<THasId>() where THasId : class, IHasId, new()
        {
            return _sessionProvider.CurrentSession.Query<THasId>();
        }
    }
}