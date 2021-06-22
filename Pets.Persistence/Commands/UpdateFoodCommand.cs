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


    public class UpdateFoodCommand : IAsyncCommand<UpdateFoodCommandContext>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public UpdateFoodCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            UpdateFoodCommandContext commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            await connection.ExecuteAsync(@"
                UPDATE Food
                SET
                    AnimalType = @AnimalType,
                    Name = @Name,
                    Count = @Count
                WHERE
                    Food.Id = @Id", new
            {
                Id = commandContext.Food.Id,
                Count = commandContext.Food.Count,
                Name = commandContext.Food.Name,
                AnimalType = commandContext.Food.AnimalType,
            }, transaction);
        }
    }
}