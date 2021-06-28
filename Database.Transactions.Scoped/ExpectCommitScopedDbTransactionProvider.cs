namespace Database.Transactions.Scoped
{
    using System;
    using Abstractions;
    using Persistence.Transactions.Behaviors;

    public class ExpectCommitScopedDbTransactionProvider : ScopedDbTransactionProvider, IExpectCommit
    {
        public ExpectCommitScopedDbTransactionProvider(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }



        public void PerformCommit()
        {
            if (Disposed)
                throw new InvalidOperationException("Object already disposed");

            try
            {
                CommitTransaction();
            }
            catch
            {
                RollbackTransaction();

                throw;
            }
            finally
            {
                Dispose();
            }
        }
    }
}