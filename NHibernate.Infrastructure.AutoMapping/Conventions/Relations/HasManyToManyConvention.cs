namespace NHibernate.Infrastructure.AutoMapping.Conventions.Relations
{
    using System.Linq;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;
    using Naming;

    public class HasManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Access.ReadOnlyPropertyThroughCamelCaseField(CamelCasePrefix.Underscore);
            instance.Cascade.SaveUpdate();
            instance.AsSet();
            instance.BatchSize(25);
            instance.Not.KeyNullable();

            string oneSideName = instance.EntityType.Name;
            string otherSideName = instance.OtherSide.EntityType.Name;
            string tableName = string.Join("To", new[] { oneSideName, otherSideName }.OrderBy(x => x));

            instance.Table(tableName);

            string oneSideFieldName = $"{tableName}_{oneSideName}{NamingConstants.IdName}";
            string otherSideFieldName = $"{oneSideName}_{NamingConstants.IdName}";

            string foreignKeyName = $"{NamingConstants.ForeignKeyPrefix}_{oneSideFieldName}_{otherSideFieldName}".Truncate();

            instance.Key.ForeignKey(foreignKeyName);
        }
    }
}