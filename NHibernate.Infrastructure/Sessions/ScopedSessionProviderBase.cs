namespace NHibernate.Infrastructure.Sessions
{
    using System;
    using System.Data;
    using Abstractions;


    public class ScopedSessionProviderBase : ISessionProvider, IDisposable
    {
        private readonly ISessionFactory _sessionFactory;

        private ISession _session;
        private ITransaction _transaction;


        public ScopedSessionProviderBase(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
        }

        ~ScopedSessionProviderBase()
        {
            Dispose(false);
        }


        protected bool Disposed { get; set; }


        public ISession CurrentSession
        {
            get
            {
                if (Disposed)
                    throw new ObjectDisposedException(nameof(ScopedSessionProviderBase));

                if (_session != null)
                    return _session;

                _session = _sessionFactory.OpenSession();
                _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);

                return _session;
            }
        }

        protected void CommitTransaction()
        {
            _transaction?.Commit();
        }

        protected void RollbackTransaction()
        {
            _transaction?.Rollback();
        }

        #region IDisposable implementation

        public virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            Disposed = true;

            if (disposing)
            {
                _transaction?.Dispose();
                _session?.Dispose();
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}