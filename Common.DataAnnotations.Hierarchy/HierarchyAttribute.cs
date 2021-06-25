namespace Common.DataAnnotations.Hierarchy
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class HierarchyAttribute : Attribute
    {
        public HierarchyAttribute(object discriminatorValue)
        {
            DiscriminatorValue = discriminatorValue ?? throw new ArgumentNullException(nameof(discriminatorValue));
        }



        public object DiscriminatorValue { get; init; }
    }
}