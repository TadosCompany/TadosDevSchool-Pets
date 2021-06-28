namespace Pets.Domain.Commands.Contexts
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Commands.Abstractions;
    using global::Domain.Abstractions;


    public class CreateObjectWithIdCommandContext<TObjectWithId> : ICommandContext 
        where TObjectWithId : class, IHasId, new()
    {
        public CreateObjectWithIdCommandContext(TObjectWithId entity)
        {
            ObjectWithId = entity ?? throw new ArgumentNullException(nameof(entity));
        }


        public TObjectWithId ObjectWithId { get; }
    }

    public static class CreateObjectWithIdCommandContextExtensions
    {
        public static Task CreateAsync<TObjectWithId>(
            this IAsyncCommandBuilder commandBuilder,
            TObjectWithId objectWithId,
            CancellationToken cancellationToken = default) where TObjectWithId : class, IHasId, new()
        {
            return commandBuilder.ExecuteAsync(
                new CreateObjectWithIdCommandContext<TObjectWithId>(objectWithId), 
                cancellationToken);
        }
    }
}