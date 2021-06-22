namespace Pets.Domain.Entities
{
    using System;
    using global::Domain.Abstractions;
    using Enums;


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

        public Food(long id, AnimalType animalType, string name, int count)
            : this(animalType, name)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Id = id;
            Count = count;
        }



        public long Id { get; set; }

        public AnimalType AnimalType { get; protected set; }

        public string Name { get; protected set; }

        public int Count { get; protected set; }



        public void Increase(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count += count;
        }

        protected internal void Decrease(int count)
        {
            if (count <= 0 || count > Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count -= count;
        }
    }
}
