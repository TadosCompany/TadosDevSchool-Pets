namespace Pets.Persistence.NHibernate
{
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using global::NHibernate.Cfg;
    using global::NHibernate.Infrastructure.AutoMapping.Configuration;
    using global::NHibernate.Infrastructure.AutoMapping.Conventions;
    using global::NHibernate.Tool.hbm2ddl;

    public class NHibernateInitializer
    {
        public static readonly string ConnectionStringParameterName = nameof(_connectionString).TrimStart('_');

        private readonly string _connectionString;


        public NHibernateInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }


        private static bool ShouldExpose => true;


        private IPersistenceConfigurer GetDatabaseConfiguration() =>
            SQLiteConfiguration
                .Standard
                .ConnectionString(_connectionString)
#if DEBUG
                .ShowSql()
#endif
        ;

        private AutoPersistenceModel GetAutoPersistenceModel() =>
            AutoMap.AssemblyOf<DomainAssemblyMarker>(new DomainAutoMappingConfiguration())
                .Conventions.AddFromAssemblyOf<IdConvention>()
                .Conventions.AddFromAssemblyOf<NHibernateInitializer>()
                .UseOverridesFromAssemblyOf<NHibernateInitializer>();

        private void Expose(Configuration configuration)
        {
#if DEBUG
            if (ShouldExpose)
            {
                new SchemaUpdate(configuration).Execute(true, true);
            }
#endif
        }


        public Configuration GetConfiguration()
        {
            return Fluently.Configure()
                .Database(GetDatabaseConfiguration)
                .Mappings(x => x.AutoMappings.Add(GetAutoPersistenceModel))
                .ExposeConfiguration(Expose)
                .BuildConfiguration();
        }
    }
}
