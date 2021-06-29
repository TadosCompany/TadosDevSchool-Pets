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


    public class CreateDogCommand : IAsyncCommand<CreateObjectWithIdCommandContext<Dog>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateDogCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateObjectWithIdCommandContext<Dog> commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            Dog dog = commandContext.ObjectWithId;
            
            dog.Id = await connection.ExecuteScalarAsync<long>(@"
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
                Type = dog.Type,
                Name = dog.Name,
                BreedId = dog.Breed.Id,
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
                AnimalId = dog.Id,
                TailLength = dog.TailLength,
            }, transaction);
        }
    }
}