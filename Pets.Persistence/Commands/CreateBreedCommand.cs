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


    public class CreateBreedCommand : IAsyncCommand<CreateBreedCommandContext>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateBreedCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateBreedCommandContext commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            commandContext.Breed.Id = await connection.ExecuteScalarAsync<long>(@"
                INSERT INTO Breed
                (
                    Name,
                    AnimalType    
                )
                VALUES
                (
                    @Name,
                    @AnimalType
                ); SELECT last_insert_rowid();", new
            {
                Name = commandContext.Breed.Name,
                AnimalType = commandContext.Breed.AnimalType,
            }, transaction);
        }
    }
}