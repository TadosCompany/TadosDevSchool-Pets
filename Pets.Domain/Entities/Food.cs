namespace Pets.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using global::Domain.Abstractions;
    using Enums;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class Food : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public Food()
        {
        }

        protected internal Food(AnimalType animalType, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            AnimalType = animalType;
            Name = name;
            Count = 0;
        }



        public virtual long Id { get; protected set; }

        public virtual AnimalType AnimalType { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual int Count { get; protected set; }



        public virtual void Increase(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count += count;
        }

        protected internal virtual void Decrease(int count)
        {
            if (count <= 0 || count > Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count -= count;
        }
    }
}
