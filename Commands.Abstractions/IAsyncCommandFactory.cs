namespace Commands.Abstractions
{
    public interface IAsyncCommandFactory
    {
        IAsyncCommand<TCommandContext> Create<TCommandContext>() where TCommandContext : ICommandContext;
    }
}