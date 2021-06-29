namespace NHibernate.Infrastructure.AutoMapping.Extensions
{
    using System;

    public static class BaseTypeExtensions
    {
        public static Type NonAbstractBaseType(this Type type)
        {
            Type baseType = type.BaseType;

            while (baseType != null)
            {
                if (!baseType.IsAbstract)
                    return baseType;

                baseType = baseType.BaseType;
            }

            return null;
        }
    }
}