namespace Pets.Persistence.NHibernate.Overrides
{
    using Domain.ValueObjects;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;
    using Types;


    public class FeedingOverride : IAutoMappingOverride<Feeding>
    {
        public void Override(AutoMapping<Feeding> mapping)
        {
            mapping.Map(x => x.DateTimeUtc).CustomType<SqliteCustomUtcDateTimeType>();
        }
    }
}