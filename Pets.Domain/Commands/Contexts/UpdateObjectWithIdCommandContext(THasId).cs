namespace Pets.Domain.Commands.Contexts
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Commands.Abstractions;
    using global::Domain.Abstractions;


    public class UpdateObjectWithIdCommandContext<THasId> : ICommandContext
        where THasId : class, IHasId, new()
    {
        public UpdateObjectWithIdCommandContext(THasId objectWithId)
        {
            ObjectWithId = objectWithId ?? throw new ArgumentNullException(nameof(objectWithId));
        }


        public THasId ObjectWithId { get; }
    }

    public static class UpdateObjectWithIdCommandContextExtensions
    {
        public static Task UpdateAsync<THasId>(
            this IAsyncCommandBuilder commandBuilder,
            THasId objectWithId,
            CancellationToken cancellationToken = default) where THasId : class, IHasId, new()
        {
            return commandBuilder.ExecuteAsync(
                new UpdateObjectWithIdCommandContext<THasId>(objectWithId),
                cancellationToken);
        }
    }
}