namespace NHibernate.Infrastructure.AutoMapping.Extensions
{
    using System;


    public static class AssignTypeExtensions
    {
        public static bool IsAssignableTo<TType>(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsAssignableTo(typeof(TType));
        }

        public static bool IsAssignableTo(this Type type, Type isAssignableToType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return isAssignableToType.IsAssignableFrom(type);
        }
    }
}