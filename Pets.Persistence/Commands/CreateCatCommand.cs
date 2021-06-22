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


    public class CreateCatCommand : IAsyncCommand<CreateCatCommandContext>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public CreateCatCommand(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task ExecuteAsync(
            CreateCatCommandContext commandContext,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            commandContext.Cat.Id = await connection.ExecuteScalarAsync<long>(@"
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
                Type = commandContext.Cat.Type,
                Name = commandContext.Cat.Name,
                BreedId = commandContext.Cat.Breed.Id,
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
                AnimalId = commandContext.Cat.Id,
                Weight = commandContext.Cat.Weight,
            }, transaction);
        }
    }
}