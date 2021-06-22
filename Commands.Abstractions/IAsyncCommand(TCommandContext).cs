namespace Commands.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;


    public interface IAsyncCommand<in TCommandContext> where TCommandContext : ICommandContext
    {
        Task ExecuteAsync(
            TCommandContext commandContext,
            CancellationToken cancellationToken = default);
    }
}