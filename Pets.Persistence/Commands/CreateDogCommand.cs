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


    public class CreateDogCommand : IAsyncCommand<CreateDogCommandContext>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateDogCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateDogCommandContext commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            commandContext.Dog.Id = await connection.ExecuteScalarAsync<long>(@"
                INSERT INTO Animal
                (
                    Type,
                    Name,
                    BreedId
                )
                VALUES 
                (
                    @Type,
                    @Name,
                    @BreedId
                ); SELECT last_insert_rowid()", new
            {
                Type = commandContext.Dog.Type,
                Name = commandContext.Dog.Name,
                BreedId = commandContext.Dog.Breed.Id,
            }, transaction);

            await connection.ExecuteAsync(@"
                INSERT INTO Dog
                (
                    AnimalId,
                    TailLength
                )
                VALUES
                (
                    @AnimalId,
                    @TailLength
                )", new
            {
                AnimalId = commandContext.Dog.Id,
                TailLength = commandContext.Dog.TailLength,
            }, transaction);
        }
    }
}