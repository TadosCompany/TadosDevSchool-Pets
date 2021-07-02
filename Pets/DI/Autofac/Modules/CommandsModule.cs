namespace Pets.DI.Autofac.Modules
{
    using Commands.Abstractions;
    using global::Autofac;
    using Persistence.ORM;
    using Persistence.ORM.Commands;
    using Tados.Autofac.Extensions.TypedFactories;

    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(CreateObjectWithIdCommand<>))
                .As(typeof(IAsyncCommand<>))
                .InstancePerDependency();

            builder
                .RegisterAssemblyTypes(typeof(PersistenceOrmAssemblyMarker).Assembly)
                .AsClosedTypesOf(typeof(IAsyncCommand<>))
                .InstancePerDependency();

            builder
                .RegisterGenericTypedFactory<IAsyncCommandFactory>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<DefaultAsyncCommandBuilder>()
                .As<IAsyncCommandBuilder>()
                .InstancePerLifetimeScope();
        }
    }
}
