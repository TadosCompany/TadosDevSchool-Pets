namespace Pets.Domain.Services.Animals.Dogs
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class DogService : AnimalServiceBase, IDogService
    {
        public DogService(
            IAsyncQueryBuilder asyncQueryBuilder,
            IAsyncCommandBuilder asyncCommandBuilder)
            : base(
                asyncQueryBuilder,
                asyncCommandBuilder)
        {
        }



        public async Task<Dog> CreateDogAsync(
            string name, 
            Breed breed, 
            Food favoriteFood,
            decimal tailLength, 
            CancellationToken cancellationToken = default)
        {
            var dog = new Dog(name, breed, favoriteFood, tailLength);

            await CreateAnimalAsync(dog, cancellationToken);
            
            return dog;
        }
    }
}
