namespace Pets.Persistence.Queries
{
    using System;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Database.Abstractions;
    using Domain.Criteria;
    using global::Queries.Abstractions;


    public class FindAnimalsCountByNameAndAnimalTypeQuery : IAsyncQuery<FindAnimalsCountByNameAndAnimalType, int>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public FindAnimalsCountByNameAndAnimalTypeQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task<int> AskAsync(
            FindAnimalsCountByNameAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            return await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM Animal WHERE Name = @Name AND Type = @AnimalType",
                new
                {
                    Name = criterion.Name,
                    AnimalType = criterion.AnimalType,
                });
        }
    }
}