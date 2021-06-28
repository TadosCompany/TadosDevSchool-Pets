namespace NHibernate.Infrastructure.AutoMapping.Conventions.Relations
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;
    using Naming;

    public class ReferenceConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Cascade.All();

            string manyPartField = $"{instance.EntityType.Name}_{instance.Name}{NamingConstants.IdName}";
            string onePartField = $"{instance.Class.Name}_{NamingConstants.IdName}";

            string foreignKeyName = $"{NamingConstants.ForeignKeyPrefix}_{manyPartField}_{onePartField}".Truncate();

            instance.ForeignKey(foreignKeyName);
        }
    }
}