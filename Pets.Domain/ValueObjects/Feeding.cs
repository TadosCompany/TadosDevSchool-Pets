namespace Pets.Domain.ValueObjects
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using global::Domain.Abstractions;
    using Entities;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class Feeding : IValueObjectWithId
    {
        [Obsolete("Only for reflection", true)]
        public Feeding()
        {
        }

        protected internal Feeding(DateTime dateTimeUtc, Food food, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            DateTimeUtc = dateTimeUtc;
            Food = food ?? throw new ArgumentNullException(nameof(food));
            Count = count;
        }



        public virtual long Id { get; protected set; }

        public virtual DateTime DateTimeUtc { get; protected set; }

        public virtual Food Food { get; protected set; }

        public virtual int Count { get; protected set; }
    }
}
