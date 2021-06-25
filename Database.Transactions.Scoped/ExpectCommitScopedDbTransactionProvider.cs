namespace Database.Transactions.Scoped
{
    using System;
    using Abstractions;
    using Persistence.Transactions.Behaviors;

    public class ExpectCommitScopedSessionProvider : ScopedDbTransactionProvider, IExpectCommit
    {
        public ExpectCommitScopedSessionProvider(IDbConnectionFactory dbConnectionFactory)
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