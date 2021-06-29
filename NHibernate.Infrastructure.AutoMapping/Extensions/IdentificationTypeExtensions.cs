namespace NHibernate.Infrastructure.AutoMapping.Extensions
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstractions;

    public static class IdentificationTypeExtensions
    {
        public static bool HasId(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return typeof(IHasId).IsAssignableFrom(type);
        }

        public static bool IsCollectionOfHasId(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsClosedGenericTypeOf(typeof(IEnumerable<>)) &&
                   type.GetGenericArguments()[0].HasId();
        }
    }
}