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


    public class FindBreedsCountByNameAndBreedTypeQuery : IAsyncQuery<FindBreedsCountByNameAndAnimalType, int>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public FindBreedsCountByNameAndBreedTypeQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task<int> AskAsync(
            FindBreedsCountByNameAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            return await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM Breed WHERE Name = @Name AND AnimalType = @AnimalType",
                new
                {
                    Name = criterion.Name,
                    AnimalType = criterion.AnimalType,
                });
        }
    }
}