namespace Pets.Domain.Services.Animals.Cats
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using global::Domain.Abstractions;

    public interface ICatService : IDomainService
    {
        Task<Cat> CreateCatAsync(
            string name,
            Breed breed,
            Food favoriteFood,
            decimal weight,
            CancellationToken cancellationToken = default);
    }
}