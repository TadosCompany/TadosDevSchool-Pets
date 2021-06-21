namespace Pets.Domain.Services.Animals
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Criteria;
    using Enums;
    using Exceptions;
    using Queries.Abstractions;

    public abstract class AnimalServiceBase
    {
        private readonly IAsyncQueryBuilder _queryBuilder;


        protected AnimalServiceBase(IAsyncQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }


        protected async Task CheckIsAnimalWithNameExistAsync(
            AnimalType animalType, 
            string name, 
            CancellationToken cancellationToken = default)
        {
            int existingCount = await _queryBuilder
                .For<int>()
                .WithAsync(
                    new FindAnimalsCountByNameAndAnimalType(name, animalType),
                    cancellationToken);

            if (existingCount != 0)
                throw new NameAlreadyExistsException();
        }
    }
}