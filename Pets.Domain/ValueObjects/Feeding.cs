namespace Pets.Domain.ValueObjects
{
    using System;
    using global::Domain.Abstractions;
    using Entities;

    public class Feeding : IValueObjectWithId
    {
        [Obsolete("Only for reflection", true)]
        public Feeding()
        {
        }

        protected internal Feeding(DateTime dateTimeUtc, Food food, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            DateTimeUtc = dateTimeUtc;
            Food = food ?? throw new ArgumentNullException(nameof(food));
            Count = count;
        }

        public Feeding(long id, DateTime dateTimeUtc, Food food, int count)
            : this(dateTimeUtc, food, count)
        {
            Id = id;
        }


        public virtual long Id { get; set; }

        public virtual DateTime DateTimeUtc { get; protected set; }

        public virtual Food Food { get; protected set; }

        public virtual int Count { get; protected set; }
    }
}