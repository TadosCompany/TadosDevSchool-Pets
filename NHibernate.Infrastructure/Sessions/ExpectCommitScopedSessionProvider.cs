namespace NHibernate.Infrastructure.Sessions
{
    using System;
    using Persistence.Transactions.Behaviors;


    public class ExpectCommitScopedSessionProvider : ScopedSessionProviderBase, IExpectCommit
    {
        public ExpectCommitScopedSessionProvider(ISessionFactory sessionFactory) : base(sessionFactory)
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