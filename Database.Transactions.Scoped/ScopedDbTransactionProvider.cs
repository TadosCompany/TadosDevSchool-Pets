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
        private bool _disposed;


        public ScopedDbTransactionProvider(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        ~ScopedDbTransactionProvider()
        {
            Dispose(false);
        }
        
        #region IDbTransactionProvider implementation

        public bool IsInitialized => !_disposed && _transaction != null;

        public async Task<DbTransaction> GetCurrentTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_disposed)
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
            if (_disposed)
                return;

            _disposed = true;

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
    }
}