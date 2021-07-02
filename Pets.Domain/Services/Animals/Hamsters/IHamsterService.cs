namespace Pets.Domain.Services.Animals.Hamsters
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using global::Domain.Abstractions;

    public interface IHamsterService : IDomainService
    {
        Task<Hamster> CreateHamsterAsync(
            string name,
            Breed breed,
            Food favoriteFood,
            string eyesColor,
            CancellationToken cancellationToken = default);
    }
}