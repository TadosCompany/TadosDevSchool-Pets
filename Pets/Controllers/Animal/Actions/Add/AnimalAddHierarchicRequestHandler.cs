namespace Pets.Controllers.Animal.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Hierarchic.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Queries.Abstractions;

    public abstract class AnimalAddHierarchicRequestHandler<TConcreteAnimalHierarchicRequest> : AsyncHierarchicRequestHandlerBase<TConcreteAnimalHierarchicRequest, AnimalAddHierarchicResponse>
        where TConcreteAnimalHierarchicRequest: AnimalAddHierarchicRequest
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;



        protected AnimalAddHierarchicRequestHandler(IAsyncQueryBuilder asyncQueryBuilder)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
        }



        protected override async Task<AnimalAddHierarchicResponse> ExecuteAsync(TConcreteAnimalHierarchicRequest request)
        {
            var breed = await _asyncQueryBuilder.FindByIdAsync<Breed>(request.BreedId);

            Food favoriteFood = null;

            if (request.FavoriteFoodId.HasValue)
            {
                favoriteFood = await _asyncQueryBuilder.FindByIdAsync<Food>(request.FavoriteFoodId.Value);
            }

            Animal animal = await CreateAnimalAsync(
                name: request.Name.Trim(),
                breed: breed,
                favoriteFood: favoriteFood,
                request: request);

            return new AnimalAddHierarchicResponse(
                Id: animal.Id);
        }



        protected abstract Task<Animal> CreateAnimalAsync(
            string name, 
            Breed breed, 
            Food favoriteFood,
            TConcreteAnimalHierarchicRequest request);
    }
}
