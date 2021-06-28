namespace Pets.Persistence.ORM.Commands.ValueObjects.Feedings
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Commands.Contexts;
    using global::Commands.Abstractions;


    public class CreateFeedingCommand : IAsyncCommand<CreateFeedingCommandContext>
    {
        public Task ExecuteAsync(
            CreateFeedingCommandContext commandContext,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}