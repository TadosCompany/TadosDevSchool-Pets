namespace NHibernate.Infrastructure.AutoMapping.Extensions
{
    using System;
    using System.Linq;

    public static class GenericTypeExtensions
    {
        public static bool IsClosedGenericTypeOf(this Type type, Type openGenericType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (openGenericType == null)
                throw new ArgumentNullException(nameof(openGenericType));

            if (!openGenericType.IsGenericTypeDefinition)
                throw new ArgumentException("Open generic type expected", nameof(openGenericType));

            return type.IsGenericType && type.GetGenericTypeDefinition() == openGenericType;
        }

        public static bool ImplementsOpenGeneric(this Type type, Type openGenericType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (openGenericType == null)
                throw new ArgumentNullException(nameof(openGenericType));

            if (!openGenericType.IsGenericTypeDefinition)
                throw new ArgumentException("Open generic type expected", nameof(openGenericType));

            return type.IsClosedGenericTypeOf(openGenericType) ||
                   type.BaseType != null && type.BaseType.ImplementsOpenGeneric(openGenericType) ||
                   type.GetInterfaces().Any(interfaceType => ImplementsOpenGeneric(interfaceType, openGenericType));
        }
    }
}