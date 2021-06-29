namespace Pets.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Domain.Abstractions;
    using Enums;
    using ValueObjects;

    public class Animal : IEntity
    {
        private readonly ICollection<Feeding> _feedings = new HashSet<Feeding>();

        [Obsolete("Only for reflection", true)]
        public Animal()
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



        public virtual long Id { get; set; }

        public virtual AnimalType Type { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual Breed Breed { get; protected set; }

        public virtual IEnumerable<Feeding> Feedings => _feedings;

        public virtual IEnumerable<Feeding> OrderedFeedings => _feedings.AsQueryable().OrderByDescending(x => x.DateTimeUtc);



        protected internal virtual Feeding Feed(Food food, int count)
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
