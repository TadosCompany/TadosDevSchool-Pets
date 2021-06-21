namespace Pets.DI.Microsoft.DependencyInjection.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Commands.Abstractions;
    using Factories;
    using global::Microsoft.Extensions.DependencyInjection;


    public static class CommandsExtensions
    {
        public static IServiceCollection AddCommandsFromAssemblyContaining<T>(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAsyncCommandFactory, ScopedAsyncCommandFactory>();
            serviceCollection.AddScoped<IAsyncCommandBuilder, DefaultAsyncCommandBuilder>();
            
            Assembly assemblyToScan = typeof(T).Assembly;
            Type commandOpenType = typeof(IAsyncCommand<>);
            
            Type[] commandTypes = assemblyToScan.ExportedTypes
                .Where(x => !x.IsAbstract && !x.IsInterface && x.GetInterfaces()
                    .Any(y => y.GetGenericTypeDefinition() is { } type && type == commandOpenType))
                .ToArray();

            foreach (Type commandType in commandTypes)
            {
                Type[] interfaceTypes = commandType.GetInterfaces()
                    .Where(x => x.GetGenericTypeDefinition() is { } type && type == commandOpenType).ToArray();

                foreach (Type interfaceType in interfaceTypes)
                {
                    serviceCollection.AddTransient(interfaceType, commandType);
                }
            }
            
            return serviceCollection;
        }
    }
}