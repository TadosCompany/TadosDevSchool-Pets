namespace NHibernate.Infrastructure.AutoMapping.Extensions
{
    using System;

    public static class NullabilityTypeExtensions
    {
        public static bool IsNullable(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsClosedGenericTypeOf(typeof(Nullable<>));
        }
    }
}