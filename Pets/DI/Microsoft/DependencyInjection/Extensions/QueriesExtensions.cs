namespace Pets.DI.Microsoft.DependencyInjection.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Factories;
    using global::Microsoft.Extensions.DependencyInjection;
    using Queries.Abstractions;

    public static class QueriesExtensions
    {
        public static IServiceCollection AddQueriesFromAssemblyContaining<T>(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IAsyncQueryFor<>), typeof(DefaultAsyncQueryFor<>));
            serviceCollection.AddScoped<IAsyncQueryFactory, ScopedAsyncQueryFactory>();
            serviceCollection.AddScoped<IAsyncQueryBuilder, ScopedAsyncQueryBuilder>();

            Assembly assemblyToScan = typeof(T).Assembly;
            Type queryOpenType = typeof(IAsyncQuery<,>);

            Type[] queryTypes = assemblyToScan.ExportedTypes
                .Where(x => !x.IsAbstract && !x.IsInterface && x.GetInterfaces()
                    .Any(y => y.GetGenericTypeDefinition() is { } type && type == queryOpenType))
                .ToArray();

            foreach (Type queryType in queryTypes)
            {
                Type[] interfaceTypes = queryType.GetInterfaces()
                    .Where(x => x.GetGenericTypeDefinition() is { } type && type == queryOpenType).ToArray();

                foreach (Type interfaceType in interfaceTypes)
                {
                    serviceCollection.AddTransient(interfaceType, queryType);
                }
            }
            
            return serviceCollection;
        }
    }
}