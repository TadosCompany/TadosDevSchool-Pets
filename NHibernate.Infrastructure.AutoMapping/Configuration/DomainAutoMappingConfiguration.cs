namespace NHibernate.Infrastructure.AutoMapping.Configuration
{
    using System;
    using Extensions;
    using FluentNHibernate;
    using FluentNHibernate.Automapping;
    using Naming;


    public class DomainAutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.HasId();
        }

        public override bool ShouldMap(Member member)
        {
            if (!member.IsProperty)
                return false;

            if (!member.IsPublic)
                return false;

            if (member.CanWrite)
                return true;

            if (member.PropertyType.IsCollectionOfHasId() && member.TryGetBackingField(out _))
                return true;

            return false;
        }
        
        public override bool IsId(Member member)
        {
            return member.Name.Equals(NamingConstants.IdName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsComponent(Type type)
        {
            return type.IsComponent();
        }
    }
}