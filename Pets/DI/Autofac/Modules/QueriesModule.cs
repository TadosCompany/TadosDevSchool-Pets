namespace Pets.DI.Autofac.Modules
{
    using global::Autofac;
    using Persistence;
    using Queries.Abstractions;
    using Tados.Autofac.Extensions.TypedFactories;

    public class QueriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(PersistenceAssemblyMarker).Assembly)
                .AsClosedTypesOf(typeof(IAsyncQuery<,>))
                .InstancePerDependency();

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
