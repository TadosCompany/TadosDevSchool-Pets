namespace Pets.Domain.Services.Animals
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

    public abstract class AnimalServiceBase
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IAsyncCommandBuilder _asyncCommandBuilder;



        protected AnimalServiceBase(IAsyncQueryBuilder asyncQueryBuilder, IAsyncCommandBuilder asyncCommandBuilder)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _asyncCommandBuilder = asyncCommandBuilder ?? throw new ArgumentNullException(nameof(asyncCommandBuilder));
        }



        protected async Task CreateAnimalAsync<TAnimal>(
            TAnimal animal,
            CancellationToken cancellationToken = default)
            where TAnimal: Animal, new()
        {
            await CheckIsAnimalWithNameExistAsync(animal.Type, animal.Name, cancellationToken);

            await _asyncCommandBuilder.CreateAsync(animal, cancellationToken);
        }



        private async Task CheckIsAnimalWithNameExistAsync(
            AnimalType animalType, 
            string name, 
            CancellationToken cancellationToken = default)
        {
            int existingCount = await _asyncQueryBuilder
                .For<int>()
                .WithAsync(
                    new FindAnimalsCountByNameAndAnimalType(name, animalType),
                    cancellationToken);

            if (existingCount != 0)
                throw new NameAlreadyExistsException();
        }
    }
}