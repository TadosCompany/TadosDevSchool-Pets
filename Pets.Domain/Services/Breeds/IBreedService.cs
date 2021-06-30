namespace Pets.Domain.Services.Breeds
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using Enums;
    using global::Domain.Abstractions;

    public interface IBreedService : IDomainService
    {
        Task<Breed> CreateBreedAsync(AnimalType animalType, string name, CancellationToken cancellationToken = default);
    }
}