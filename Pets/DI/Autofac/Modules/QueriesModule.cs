namespace Pets.DI.Autofac.Modules
{
    using global::Autofac;
    using global::Autofac.Extensions.ConfiguredModules;
    using Microsoft.Extensions.Configuration;
    using Persistence;
    using Persistence.ORM;
    using Persistence.ORM.Queries;
    using Queries.Abstractions;
    using Tados.Autofac.Extensions.TypedFactories;

    public class QueriesModule : ConfiguredModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            bool useOrm = Configuration.GetValue("UseORM", false);

            if (!useOrm)
            {
                builder
                    .RegisterAssemblyTypes(typeof(PersistenceAssemblyMarker).Assembly)
                    .AsClosedTypesOf(typeof(IAsyncQuery<,>))
                    .InstancePerDependency();    
            }
            else
            {
                builder
                    .RegisterGeneric(typeof(FindObjectWithIdByIdQuery<>))
                    .As(typeof(IAsyncQuery<,>))
                    .InstancePerDependency();
                
                builder
                    .RegisterAssemblyTypes(typeof(PersistenceOrmAssemblyMarker).Assembly)
                    .AsClosedTypesOf(typeof(IAsyncQuery<,>))
                    .InstancePerDependency();
            }

            builder
                .RegisterGeneric(typeof(DefaultAsyncQueryFor<>))
                .As(typeof(IAsyncQueryFor<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterGenericTypedFactory<IAsyncQueryFactory>()
                .InstancePerLifetimeScope();

            builder
                .RegisterGenericTypedFactory<IAsyncQueryBuilder>()
                .InstancePerLifetimeScope();
        }
    }
}
