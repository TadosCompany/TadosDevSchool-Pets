namespace Pets.Domain.Services.Feedings
{
    using Entities;
    using global::Domain.Abstractions;

    public interface IFeedingService : IDomainService
    {
        void Feed(Animal animal, Food food, int count);
    }
}