namespace Pets.Domain.Services.Feedings
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using global::Domain.Abstractions;


    public interface IFeedingService : IDomainService
    {
        Task FeedAsync(Animal animal, Food food, int count, CancellationToken cancellationToken = default);
    }
}