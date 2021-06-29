namespace NHibernate.Infrastructure.AutoMapping.Extensions
{
    using System;
    using Domain.Abstractions;

    public static class MappingTypeExtensions
    {
        public static bool IsValueObject(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsAssignableTo<IValueObject>();
        }
        
        public static bool IsComponent(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsValueObject() && !type.HasId();
        }
    }
}