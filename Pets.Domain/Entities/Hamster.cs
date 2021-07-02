namespace Pets.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Enums;
    using Exceptions;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class Hamster : Animal
    {
        [Obsolete("Only for reflection", true)]
        public Hamster()
        {
        }
        
        protected internal Hamster(string name, Breed breed, Food favoriteFood, string eyesColor)
            : base(AnimalType.Hamster, name, breed, favoriteFood)
        {
            if (string.IsNullOrWhiteSpace(eyesColor))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(eyesColor));

            EyesColor = eyesColor;
        }



        public virtual string EyesColor { get; protected set; }



        protected internal override void Feed(Food food, int count)
        {
            int sameFoodThisDayFeedingsCount = Feedings.Count(f => f.DateTimeUtc >= DateTime.UtcNow.AddDays(-1) && f.Food == food);

            if (sameFoodThisDayFeedingsCount != 0)
                throw new AnotherFoodRequiredException();

            base.Feed(food, count);
        }
    }
}
