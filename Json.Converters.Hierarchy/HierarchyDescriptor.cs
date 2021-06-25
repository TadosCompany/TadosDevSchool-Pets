namespace Json.Converters.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Common.DataAnnotations.Hierarchy;

    internal class HierarchyDescriptor
    {
        public HierarchyDescriptor(Type baseType)
        {
            if (baseType == null)
                throw new ArgumentNullException(nameof(baseType));

            DiscriminatorProperty = baseType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .SingleOrDefault(x => x.GetCustomAttribute<HierarchyDiscriminatorAttribute>() != null)
                ?? throw new ArgumentException("BaseType has no HierarchyDiscriminatorAttribute property", nameof(baseType));

            NestedTypes = baseType
                .Assembly
                .ExportedTypes
                .Where(x => baseType.IsAssignableFrom(x) && x.GetCustomAttribute<HierarchyAttribute>() != null)
                .ToDictionary(x => x.GetCustomAttribute<HierarchyAttribute>().DiscriminatorValue);
        }



        public PropertyInfo DiscriminatorProperty { get; init; }

        public IReadOnlyDictionary<object, Type> NestedTypes { get; init; }
    }
}