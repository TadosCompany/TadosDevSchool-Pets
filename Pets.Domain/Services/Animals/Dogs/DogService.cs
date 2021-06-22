namespace Pets.Domain.Services.Animals.Dogs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.Contexts;
    using Entities;
    using Enums;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class DogService : AnimalServiceBase, IDogService
    {
        private readonly IAsyncCommandBuilder _asyncCommandBuilder;


        public DogService(IAsyncQueryBuilder asyncQueryBuilder, IAsyncCommandBuilder asyncCommandBuilder)
            : base(asyncQueryBuilder)
        {
            _asyncCommandBuilder = asyncCommandBuilder ?? throw new ArgumentNullException(nameof(asyncCommandBuilder));
        }


        public async Task<Dog> CreateDogAsync(string name, Breed breed, decimal tailLength, CancellationToken cancellationToken = default)
        {
            await CheckIsAnimalWithNameExistAsync(AnimalType.Dog, name, cancellationToken);

            Dog dog = new Dog(name, breed, tailLength);

            await _asyncCommandBuilder.ExecuteAsync(new CreateDogCommandContext(dog), cancellationToken);
            
            return dog;
        }
    }
}