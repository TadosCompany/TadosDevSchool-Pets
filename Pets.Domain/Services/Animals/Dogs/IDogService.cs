namespace Pets.Domain.Services.Animals.Dogs
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using global::Domain.Abstractions;

    public interface IDogService : IDomainService
    {
        Task<Dog> CreateDogAsync(
            string name,
            Breed breed,
            decimal tailLength,
            CancellationToken cancellationToken = default);
    }
}