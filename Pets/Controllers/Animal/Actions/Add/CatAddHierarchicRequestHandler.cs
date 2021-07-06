namespace Pets.Controllers.Animal.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Services.Animals.Cats;
    using Queries.Abstractions;

    public class CatAddHierarchicRequestHandler : AnimalAddHierarchicRequestHandler<CatAddHierarchicRequest>
    {
        private readonly ICatService _catService;



        public CatAddHierarchicRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, ICatService catService)
            : base(asyncQueryBuilder)
        {
            _catService = catService ?? throw new ArgumentNullException(nameof(catService));
        }



        protected override async Task<Animal> CreateAnimalAsync(
            string name,
            Breed breed,
            Food favoriteFood,
            CatAddHierarchicRequest request)
        {
            Cat cat = await _catService.CreateCatAsync(
                name: name,
                breed: breed,
                favoriteFood: favoriteFood,
                weight: request.Weight);

            return cat;
        }
    }
}
