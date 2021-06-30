namespace Pets.Domain.Services.Breeds
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.Contexts;
    using Criteria;
    using Entities;
    using Enums;
    using Exceptions;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class BreedService : IBreedService
    {
        private readonly IAsyncQueryBuilder _queryBuilder;
        private readonly IAsyncCommandBuilder _commandBuilder;



        public BreedService(IAsyncQueryBuilder queryBuilder, IAsyncCommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            _commandBuilder = commandBuilder ?? throw new ArgumentNullException(nameof(commandBuilder));
        }



        public async Task<Breed> CreateBreedAsync(AnimalType animalType, string name, CancellationToken cancellationToken = default)
        {
            await CheckIsBreedWithNameExistAsync(animalType, name, cancellationToken);

            var breed = new Breed(animalType, name);

            await _commandBuilder.CreateAsync(breed, cancellationToken);
            
            return breed;
        }



        private async Task CheckIsBreedWithNameExistAsync(AnimalType animalType, string name, CancellationToken cancellationToken = default)
        {
            int existingCount = await _queryBuilder
                .For<int>()
                .WithAsync(new FindBreedsCountByNameAndAnimalType(name, animalType), cancellationToken);
            
            if (existingCount != 0)
                throw new NameAlreadyExistsException();
        }
    }
}