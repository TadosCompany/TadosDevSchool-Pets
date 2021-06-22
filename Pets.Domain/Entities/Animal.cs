namespace Pets.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Domain.Abstractions;
    using Enums;
    using ValueObjects;

    public abstract class Animal : IEntity
    {
        private readonly ISet<Feeding> _feedings = new HashSet<Feeding>();

        [Obsolete("Only for reflection", true)]
        protected Animal()
        {
        }

        protected Animal(AnimalType type, string name, Breed breed)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            if (breed == null) 
                throw new ArgumentNullException(nameof(breed));

            if (breed.AnimalType != type)
                throw new ArgumentException($"Breed animal type expected to be {type}", nameof(breed));

            Type = type;
            Name = name;
            Breed = breed;
        }

        protected Animal(long id, AnimalType type, string name, Breed breed, IEnumerable<Feeding> feedings)
            : this(type, name, breed)
        {
            if (feedings == null) 
                throw new ArgumentNullException(nameof(feedings));
        
            Id = id;
        
            foreach (var feeding in feedings)
            {
                _feedings.Add(feeding);
            }
        }



        public long Id { get; set; }

        public AnimalType Type { get; init; }

        public string Name { get; init; }

        public Breed Breed { get; init; }

        public IEnumerable<Feeding> Feedings => _feedings.AsEnumerable();



        protected internal Feeding Feed(Food food, int count)
        {
            if (food == null) 
                throw new ArgumentNullException(nameof(food));

            if (count <= 0) 
                throw new ArgumentOutOfRangeException(nameof(count));

            if (food.AnimalType != Type)
                throw new ArgumentException($"Food animal type expected to be {Type}", nameof(food));

            Feeding feeding = new Feeding(DateTime.UtcNow, food, count);
            
            _feedings.Add(feeding);

            return feeding;
        }
    }
}
