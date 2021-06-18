namespace Pets.Domain.Services
{
    using System;
    using global::Domain.Abstractions;
    using Entities;
    using Exceptions;

    public class FeedingService : IDomainService
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

            animal.Feed(food, count);
            food.Decrease(count);
        }
    }
}
