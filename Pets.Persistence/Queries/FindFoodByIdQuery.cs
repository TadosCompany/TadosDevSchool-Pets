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


    public class FindFoodByIdQuery : IAsyncQuery<FindById, Food>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public FindFoodByIdQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task<Food> AskAsync(
            FindById criterion,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            return await connection.QuerySingleOrDefaultAsync<Food>(@"SELECT Id, Name, AnimalType, Count FROM Food WHERE Id = @Id", new
            {
                Id = criterion.Id,
            }, transaction);
        }
    }
}