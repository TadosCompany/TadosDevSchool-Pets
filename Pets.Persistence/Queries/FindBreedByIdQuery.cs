namespace Pets.Persistence.Queries
{
    using System;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Database.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using global::Queries.Abstractions;


    public class FindBreedByIdQuery : IAsyncQuery<FindById, Breed>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public FindBreedByIdQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task<Breed> AskAsync(FindById criterion, CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            return await connection.QuerySingleOrDefaultAsync<Breed>(@"SELECT Id, Name, AnimalType FROM Breed WHERE Id = @Id", new
            {
                Id = criterion.Id,
            }, transaction);
        }
    }
}