namespace Pets.DI.Microsoft.DependencyInjection.Extensions
{
    using Database.Abstractions;
    using global::Microsoft.Extensions.DependencyInjection;

    
    
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase<TDbConnectionFactory, TDbTransactionProvider>(this IServiceCollection serviceCollection)
            where TDbConnectionFactory : class, IDbConnectionFactory
            where TDbTransactionProvider : class, IDbTransactionProvider
        {
            serviceCollection.AddSingleton<IDbConnectionFactory, TDbConnectionFactory>();
            serviceCollection.AddScoped<IDbTransactionProvider, TDbTransactionProvider>();
            
            return serviceCollection;
        } 
    }
}