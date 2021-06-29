namespace NHibernate.Infrastructure.AutoMapping.Conventions.Relations
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;
    using Naming;


    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Access.ReadOnlyPropertyThroughCamelCaseField(CamelCasePrefix.Underscore);
            instance.Cascade.AllDeleteOrphan();
            instance.AsSet();
            instance.BatchSize(25);
            instance.Not.KeyNullable();

            if (instance.OtherSide == null)
            {
                instance.Not.Inverse();
            }
            else
            {
                instance.Inverse();
            }

            string onePartField =
                $"{instance.ChildType.Name}_{(instance.OtherSide != null ? instance.OtherSide.Property.Name : instance.EntityType.Name)}{NamingConstants.IdName}";
            string manyPartField = $"{instance.EntityType.Name}_{NamingConstants.IdName}";

            string foreignKeyName = $"{NamingConstants.ForeignKeyPrefix}_{onePartField}_{manyPartField}".Truncate();

            instance.Key.ForeignKey(foreignKeyName);
        }
    }
}