namespace NHibernate.Infrastructure.AutoMapping.Extensions
{
    using System;
    using System.Linq;
    using FluentNHibernate;

    public static class MemberTypesExtensions
    {
        public static bool IsOfType<TType>(this Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            return member.IsOfType(typeof(TType));
        }

        public static bool IsOfType<TType1, TType2>(this Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            return member.IsOfType(typeof(TType1), typeof(TType2));
        }

        public static bool IsOfType<TType1, TType2, TType3>(this Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            return member.IsOfType(typeof(TType1), typeof(TType2), typeof(TType3));
        }

        public static bool IsOfType(this Member member, params Type[] types)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            return types.Contains(member.PropertyType);
        }
    }
}