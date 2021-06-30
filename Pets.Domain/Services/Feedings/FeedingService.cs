namespace Pets.Domain.Services.Feedings
{
    using System;
    using Entities;
    using Exceptions;
    using ValueObjects;

    public class FeedingService : IFeedingService
    {
        public void Feed(Animal animal, Food food, int count)
        {
            if (animal == null)
                throw new ArgumentNullException(nameof(animal));

            if (food == null)
                throw new ArgumentNullException(nameof(food));

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (food.Count < count)
                throw new NotEnoughFoodException();

            if (food.AnimalType != animal.Type)
                throw new InvalidOperationException("Selected food can't be used for animal");

            animal.Feed(food, count);
            food.Decrease(count);
        }
    }
}