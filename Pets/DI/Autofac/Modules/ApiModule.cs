namespace Pets.DI.Autofac.Modules
{
    using Api.Requests.Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using global::Autofac;
    using Tados.Autofac.Extensions.TypedFactories;

    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(ApplicationAssemblyMarker).Assembly)
                .AsClosedTypesOf(typeof(IAsyncRequestHandler<>))
                .InstancePerDependency();

            builder
                .RegisterAssemblyTypes(typeof(ApplicationAssemblyMarker).Assembly)
                .AsClosedTypesOf(typeof(IAsyncRequestHandler<,>))
                .InstancePerDependency();

            builder
                .RegisterGenericTypedFactory<IAsyncRequestHandlerFactory>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<DefaultAsyncRequestBuilder>()
                .As<IAsyncRequestBuilder>()
                .InstancePerLifetimeScope();


            builder
                .RegisterAssemblyTypes(typeof(ApplicationAssemblyMarker).Assembly)
                .AsClosedTypesOf(typeof(IAsyncHierarchicRequestHandler<>))
                .InstancePerDependency();
            builder
                .RegisterAssemblyTypes(typeof(ApplicationAssemblyMarker).Assembly)
                .AsClosedTypesOf(typeof(IAsyncHierarchicRequestHandler<,>))
                .InstancePerDependency();

            builder
                .RegisterRuntimeTypedFactory<IAsyncHierarchicRequestHandlerFactory>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<DefaultAsyncHierarchicRequestBuilder>()
                .As<IAsyncHierarchicRequestBuilder>()
                .InstancePerLifetimeScope();
        }
    }
}
