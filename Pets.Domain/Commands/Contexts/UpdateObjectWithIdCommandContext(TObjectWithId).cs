namespace Pets.Domain.Commands.Contexts
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Commands.Abstractions;
    using global::Domain.Abstractions;


    public class UpdateObjectWithIdCommandContext<TObjectWithId> : ICommandContext
        where TObjectWithId : IHasId
    {
        public UpdateObjectWithIdCommandContext(TObjectWithId objectWithId)
        {
            ObjectWithId = objectWithId ?? throw new ArgumentNullException(nameof(objectWithId));
        }


        public TObjectWithId ObjectWithId { get; }
    }

    public static class UpdateObjectWithIdCommandContextExtensions
    {
        public static Task UpdateAsync<TObjectWithId>(
            this IAsyncCommandBuilder commandBuilder,
            TObjectWithId objectWithId,
            CancellationToken cancellationToken = default) where TObjectWithId : IHasId
        {
            return commandBuilder.ExecuteAsync(
                new UpdateObjectWithIdCommandContext<TObjectWithId>(objectWithId),
                cancellationToken);
        }
    }
}