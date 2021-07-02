namespace Pets.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Enums;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class Cat : Animal
    {
        [Obsolete("Only for reflection", true)]
        public Cat()
        {
        }
        
        protected internal Cat(string name, Breed breed, Food favoriteFood, decimal weight)
            : base(AnimalType.Cat, name, breed, favoriteFood)
        {
            if (weight < 0) 
                throw new ArgumentOutOfRangeException(nameof(weight));

            Weight = weight;
        }



        public virtual decimal Weight { get; protected set; }



        protected internal override void Feed(Food food, int count)
        {
            base.Feed(food, count);

            Weight += 1;
        }
    }
}
