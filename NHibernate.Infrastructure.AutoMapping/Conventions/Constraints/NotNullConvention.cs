namespace NHibernate.Infrastructure.AutoMapping.Conventions.Constraints
{
    using Common.DataAnnotations;
    using Extensions;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;

    public class NotNullConvention :
        IPropertyConventionAcceptance, IPropertyConvention,
        IReferenceConventionAcceptance, IReferenceConvention,
        IComponentConventionAcceptance, IComponentConvention
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria
                .Expect(inspector => !inspector.Property.PropertyType.IsNullable())
                .Expect(inspector => !inspector.Property.HasAttribute<NullableAttribute>())
                .Expect(inspector => !inspector.Property.DeclaringType.IsComponent());
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }


        public void Accept(IAcceptanceCriteria<IManyToOneInspector> criteria)
        {
            criteria
                .Expect(inspector => !inspector.Property.HasAttribute<NullableAttribute>());
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Not.Nullable();
        }


        public void Accept(IAcceptanceCriteria<IComponentInspector> criteria)
        {
            criteria
                .Expect(inspector => !inspector.Property.HasAttribute<NullableAttribute>());
        }

        public void Apply(IComponentInstance instance)
        {
            foreach (IPropertyInstance propertyInstance in instance.Properties)
            {
                if (propertyInstance.Property.PropertyType.IsNullable())
                    break;

                // if (propertyInstance.Property.HasAttribute<NullableAttribute>())
                //     break;

                propertyInstance.Not.Nullable();
            }
        }
    }
}