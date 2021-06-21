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


    public class CreateFeedingCommand : IAsyncCommand<CreateFeedingCommandContext>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateFeedingCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateFeedingCommandContext commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            await connection.ExecuteAsync(@"
                INSERT INTO Feeding
                (
                    AnimalId,
                    FoodId,
                    DateTimeUtc,
                    Count
                )
                VALUES
                (
                    @AnimalId,
                    @FoodId,
                    @DateTimeUtc,
                    @Count
                );", new
            {
                AnimalId = commandContext.Animal.Id,
                FoodId = commandContext.Feeding.Food.Id,
                DateTimeUtc = Helpers.FormatDateTime(commandContext.Feeding.DateTimeUtc),
                Count = commandContext.Feeding.Count,
            }, transaction);
        }
    }
}