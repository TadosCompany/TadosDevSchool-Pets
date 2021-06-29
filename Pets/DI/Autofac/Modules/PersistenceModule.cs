namespace Pets.DI.Autofac.Modules
{
    using Database.Abstractions;
    using Database.Sqlite;
    using Database.Transactions.Scoped;
    using global::Autofac;
    using global::Autofac.Extensions.ConfiguredModules;
    using global::Persistence.Transactions.Behaviors;
    using Linq.AsyncQueryable.Abstractions.Factories;
    using Linq.Providers.Abstractions;
    using Microsoft.Extensions.Configuration;
    using NHibernate.Infrastructure.Linq.AsyncQueryable.Factories;
    using NHibernate.Infrastructure.Linq.Providers;
    using NHibernate.Infrastructure.Repositories;
    using NHibernate.Infrastructure.Sessions;
    using NHibernate.Infrastructure.Sessions.Abstractions;
    using Persistence;
    using Persistence.NHibernate;
    using Repositories.Abstractions;
    using Startables;

    public class PersistenceModule : ConfiguredModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            string connectionString = Configuration.GetConnectionString("Pets");

            bool useOrm = Configuration.GetValue("UseORM", false);

            if (!useOrm)
            {
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
                    .RegisterType<ExpectCommitScopedDbTransactionProvider>()
                    .As<IDbTransactionProvider>()
                    .As<IExpectCommit>()
                    .InstancePerLifetimeScope();

                builder
                    .RegisterType<DatabaseStartable>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
            else
            {
                builder
                    .RegisterGeneric(typeof(NHibernateAsyncRepository<>))
                    .As(typeof(IAsyncRepository<>))
                    .InstancePerDependency();

                builder
                    .RegisterType<NHibernateLinqProvider>()
                    .As<ILinqProvider>()
                    .InstancePerDependency();

                builder
                    .RegisterType<ExpectCommitScopedSessionProvider>()
                    .As<ISessionProvider>()
                    .As<IExpectCommit>()
                    .InstancePerLifetimeScope();

                builder
                    .RegisterType<NHibernateAsyncQueryableFactory>()
                    .As<IAsyncQueryableFactory>()
                    .SingleInstance();

                builder
                    .RegisterType<NHibernateInitializer>()
                    .AsSelf()
                    .SingleInstance()
                    .WithParameter(NHibernateInitializer.ConnectionStringParameterName, connectionString);

                builder
                    .Register(context =>
                        context.Resolve<NHibernateInitializer>().GetConfiguration().BuildSessionFactory())
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }
    }
}