namespace Pets.Persistence.Commands
{
    using System;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Domain.Commands.Contexts;
    using Domain.Entities;
    using global::Commands.Abstractions;
    using global::Database.Abstractions;

    public class CreateFoodCommand : IAsyncCommand<CreateObjectWithIdCommandContext<Food>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateFoodCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateObjectWithIdCommandContext<Food> commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            Food food = commandContext.ObjectWithId;
            
            food.Id = await connection.ExecuteScalarAsync<long>(@"INSERT INTO Food
                (
                    Name,
                    AnimalType,
                    Count    
                )
                VALUES
                (
                    @Name,
                    @AnimalType,
                    @Count
                ); SELECT last_insert_rowid();", new
            {
                Name = food.Name,
                AnimalType = food.AnimalType,
                Count = food.Count,
            }, transaction);
        }
    }
}