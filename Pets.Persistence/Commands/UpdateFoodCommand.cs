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


    public class UpdateFoodCommand : IAsyncCommand<UpdateObjectWithIdCommandContext<Food>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public UpdateFoodCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            UpdateObjectWithIdCommandContext<Food> commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            Food food = commandContext.ObjectWithId;
            
            await connection.ExecuteAsync(@"
                UPDATE Food
                SET
                    AnimalType = @AnimalType,
                    Name = @Name,
                    Count = @Count
                WHERE
                    Food.Id = @Id", new
            {
                Id = food.Id,
                Count = food.Count,
                Name = food.Name,
                AnimalType = food.AnimalType,
            }, transaction);
        }
    }
}