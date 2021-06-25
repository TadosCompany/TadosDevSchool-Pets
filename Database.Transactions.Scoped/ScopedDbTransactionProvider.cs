namespace Database.Transactions.Scoped
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;


    public class ScopedDbTransactionProvider : IDbTransactionProvider, IDisposable
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        private DbConnection _connection;
        private DbTransaction _transaction;

        protected bool Disposed;



        public ScopedDbTransactionProvider(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        ~ScopedDbTransactionProvider()
        {
            Dispose(false);
        }



        private bool IsInitialized => !Disposed && _transaction != null;



        #region IDbTransactionProvider implementation

        public async Task<DbTransaction> GetCurrentTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (Disposed)
                throw new ObjectDisposedException(nameof(ScopedDbTransactionProvider));

            if (_transaction != null)
                return _transaction;

            _connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            _transaction = await _connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            return _transaction;
        }

        #endregion



        #region IDisposable implementation

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            Disposed = true;

            if (disposing)
            {
                _transaction?.Dispose();
                _connection?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion



        protected void CommitTransaction()
        {
            if (!IsInitialized)
                return;

            _transaction.Commit();
        }

        protected void RollbackTransaction()
        {
            if (!IsInitialized)
                return;

            _transaction.Rollback();
        }
    }
}