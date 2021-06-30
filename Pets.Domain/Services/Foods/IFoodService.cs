namespace Pets.Domain.Services.Foods
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using Enums;
    using global::Domain.Abstractions;

    public interface IFoodService : IDomainService
    {
        Task<Food> CreateFoodAsync(AnimalType animalType, string name, CancellationToken cancellationToken = default);
    }
}