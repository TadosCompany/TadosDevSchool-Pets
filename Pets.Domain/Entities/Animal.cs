namespace Pets.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Common.DataAnnotations;
    using global::Domain.Abstractions;
    using Enums;
    using ValueObjects;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class Animal : IEntity
    {
        private readonly ICollection<Feeding> _feedings = new HashSet<Feeding>();



        [Obsolete("Only for reflection", true)]
        public Animal()
        {
        }

        protected Animal(AnimalType type, string name, Breed breed, Food favoriteFood)
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
            SetFavoriteFood(favoriteFood);
        }



        public virtual long Id { get; protected set; }

        public virtual AnimalType Type { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual Breed Breed { get; protected set; }

        [Nullable]
        public virtual Food FavoriteFood { get; protected set; }

        public virtual IEnumerable<Feeding> Feedings => _feedings;

        public virtual IEnumerable<Feeding> OrderedFeedings => _feedings.AsQueryable().OrderByDescending(x => x.DateTimeUtc);



        public virtual void SetFavoriteFood(Food food)
        {
            if (food is not null && food.AnimalType != Type)
                throw new ArgumentException($"Food animal type expected to be {Type}", nameof(food));

            FavoriteFood = food;
        }

        protected internal virtual void Feed(Food food, int count)
        {
            if (food == null) 
                throw new ArgumentNullException(nameof(food));

            if (count <= 0) 
                throw new ArgumentOutOfRangeException(nameof(count));

            if (food.AnimalType != Type)
                throw new ArgumentException($"Food animal type expected to be {Type}", nameof(food));

            var feeding = new Feeding(DateTime.UtcNow, food, count);
            
            _feedings.Add(feeding);
        }
    }
}
