namespace NHibernate.Infrastructure.AutoMapping.Conventions.Constraints
{
    using Extensions;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;
    using Naming;


    public class ForeignKeyConvention : 
        IReferenceConvention, 
        IHasManyToManyConvention, 
        IJoinedSubclassConvention,
        IJoinConvention, 
        ICollectionConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            string keyName = instance.Property.Name + NamingConstants.IdName;

            instance.Column(keyName);
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            string keyName1 = instance.EntityType.Name + NamingConstants.IdName;
            string keyName2 = instance.ChildType.Name + NamingConstants.IdName;

            instance.Key.Column(keyName1);
            instance.Relationship.Column(keyName2);
        }

        public void Apply(IJoinedSubclassInstance instance)
        {
            string keyName = instance.Type.NonAbstractBaseType().Name + NamingConstants.IdName;

            instance.Key.Column(keyName);
        }

        public void Apply(IJoinInstance instance)
        {
            string keyName = instance.EntityType.Name + NamingConstants.IdName;

            instance.Key.Column(keyName);
        }

        public void Apply(ICollectionInstance instance)
        {
            string keyName = instance.EntityType.Name + NamingConstants.IdName;

            instance.Key.Column(keyName);
        }
    }
}