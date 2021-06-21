namespace Pets.DI.Microsoft.DependencyInjection.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using global::Domain.Abstractions;
    using global::Microsoft.Extensions.DependencyInjection;

    public static class DomainServicesExtensions
    {
        public static IServiceCollection AddDomainServicesFromAssemblyContaining<T>(
            this IServiceCollection serviceCollection)
        {
            Assembly assemblyToScan = typeof(T).Assembly;
            Type domainServiceType = typeof(IDomainService);

            Type[] domainServiceTypes = assemblyToScan.ExportedTypes
                .Where(x => !x.IsAbstract && !x.IsInterface && x.IsAssignableTo(domainServiceType))
                .ToArray();

            foreach (Type type in domainServiceTypes)
            {
                foreach (Type @interface in type.GetInterfaces().Where(x => x.IsAssignableTo(domainServiceType)))
                {
                    serviceCollection.AddScoped(@interface, type);
                }
            }
            
            return serviceCollection;
        }
    }
}