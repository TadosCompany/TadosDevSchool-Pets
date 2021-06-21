namespace Pets.DI.Microsoft.DependencyInjection.Factories
{
    using System;
    using Commands.Abstractions;
    using global::Microsoft.Extensions.DependencyInjection;


    public class ScopedAsyncCommandFactory : IAsyncCommandFactory
    {
        private readonly IServiceProvider _serviceProvider;


        public ScopedAsyncCommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public IAsyncCommand<TCommandContext> Create<TCommandContext>() where TCommandContext : ICommandContext
        {
            return _serviceProvider.GetService<IAsyncCommand<TCommandContext>>();
        }
    }
}