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


    public class CreateCatCommand : IAsyncCommand<CreateObjectWithIdCommandContext<Cat>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateCatCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateObjectWithIdCommandContext<Cat> commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            Cat cat = commandContext.ObjectWithId;
            
            cat.Id = await connection.ExecuteScalarAsync<long>(@"
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
                Type = cat.Type,
                Name = cat.Name,
                BreedId = cat.Breed.Id,
            }, transaction);

            await connection.ExecuteAsync(@"
                INSERT INTO Cat
                (
                    AnimalId,
                    Weight
                )
                VALUES
                (
                    @AnimalId,
                    @Weight
                )", new
            {
                AnimalId = cat.Id,
                Weight = cat.Weight,
            }, transaction);
        }
    }
}