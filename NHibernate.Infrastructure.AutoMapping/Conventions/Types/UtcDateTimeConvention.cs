namespace NHibernate.Infrastructure.AutoMapping.Conventions.Types
{
    using System;
    using Extensions;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;
    using Type;


    public class UtcDateTimePropertyConvention : IPropertyConventionAcceptance, IPropertyConvention
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria
                .Expect(inspector => inspector.Property.IsOfType<DateTime, DateTime?>())
                .Any(
                    // subcriteria =>
                    //     subcriteria.Expect(inspector => inspector.Property.HasAttribute<UtcDateTimeAttribute>()),
                    subcriteria => subcriteria.Expect(inspector => inspector.Property.Name.EndsWith("Utc")));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType<UtcDateTimeType>();
        }
    }
}