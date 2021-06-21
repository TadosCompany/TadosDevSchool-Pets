namespace Pets.Domain.Entities
{
    using System;
    using global::Domain.Abstractions;
    using Enums;

    public class Breed : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public Breed()
        {
        }
        
        protected internal Breed(AnimalType animalType, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            AnimalType = animalType;
            Name = name;
        }

        public Breed(long id, AnimalType animalType, string name)
            : this(animalType, name)
        {
            Id = id;
        }



        public long Id { get; set; }

        public AnimalType AnimalType { get; protected set; }

        public string Name { get; protected set; }
    }
}