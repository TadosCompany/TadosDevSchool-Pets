namespace Commands.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;


    public interface IAsyncCommandBuilder
    {
        Task ExecuteAsync<TCommandContext>(
            TCommandContext commandContext,
            CancellationToken cancellationToken = default) where TCommandContext : ICommandContext;
    }
}