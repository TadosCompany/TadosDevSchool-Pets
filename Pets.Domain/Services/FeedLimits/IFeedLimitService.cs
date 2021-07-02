namespace Pets.Domain.Services.FeedLimits
{
    using System.Threading.Tasks;
    using Entities;
    using global::Domain.Abstractions;

    public interface IFeedLimitService : IDomainService
    {
        Task<FeedLimit> CreateFeedLimitAsync(Breed breed, int maxPerDay);
    }
}
