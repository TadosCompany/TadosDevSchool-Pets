namespace Database.Abstractions
{
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDbTransactionProvider
    {
        Task<DbTransaction> GetCurrentTransactionAsync(CancellationToken cancellationToken = default);
    }
}