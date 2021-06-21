namespace Pets.Persistence.Commands
{
    using System;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Database.Abstractions;
    using Domain.Commands.Contexts;
    using global::Commands.Abstractions;

    public class CreateFoodCommand : IAsyncCommand<CreateFoodCommandContext>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateFoodCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateFoodCommandContext commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            commandContext.Food.Id = await connection.ExecuteScalarAsync<long>(@"INSERT INTO Food
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
                Name = commandContext.Food.Name,
                AnimalType = commandContext.Food.AnimalType,
                Count = commandContext.Food.Count,
            }, transaction);
        }
    }
}