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


    public class CreateBreedCommand : IAsyncCommand<CreateObjectWithIdCommandContext<Breed>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateBreedCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateObjectWithIdCommandContext<Breed> commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            Breed breed = commandContext.ObjectWithId;

            breed.Id = await connection.ExecuteScalarAsync<long>(@"
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
                Name = breed.Name,
                AnimalType = breed.AnimalType,
            }, transaction);
        }
    }
}