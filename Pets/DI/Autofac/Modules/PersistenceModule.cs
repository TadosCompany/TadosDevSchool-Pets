namespace Pets.DI.Autofac.Modules
{
    using Database.Abstractions;
    using Database.Sqlite;
    using Database.Transactions.Scoped;
    using global::Autofac;
    using global::Autofac.Extensions.ConfiguredModules;
    using Microsoft.Extensions.Configuration;
    using Persistence;

    public class PersistenceModule : ConfiguredModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            string connectionString = Configuration.GetConnectionString("Pets");


            builder
                .RegisterType<Database>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<SqliteConnectionFactory>()
                .As<IDbConnectionFactory>()
                .WithParameter(SqliteConnectionFactory.ConnectionStringParameterName, connectionString)
                .SingleInstance();

            builder
                .RegisterType<ScopedDbTransactionProvider>()
                .As<IDbTransactionProvider>()
                .InstancePerLifetimeScope();
        }
    }
}
