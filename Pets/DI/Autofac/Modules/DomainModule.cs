namespace Pets.DI.Autofac.Modules
{
    using Domain;
    using global::Autofac;
    using global::Domain.Abstractions;

    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(DomainAssemblyMarker).Assembly)
                .AssignableTo<IDomainService>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
