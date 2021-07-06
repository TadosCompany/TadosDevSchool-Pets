namespace Pets.Controllers.Animal.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Services.Animals.Dogs;
    using Queries.Abstractions;

    public class DogAddHierarchicRequestHandler : AnimalAddHierarchicRequestHandler<DogAddHierarchicRequest>
    {
        private readonly IDogService _dogService;



        public DogAddHierarchicRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IDogService dogService)
            : base(asyncQueryBuilder)
        {
            _dogService = dogService ?? throw new ArgumentNullException(nameof(dogService));
        }



        protected override async Task<Animal> CreateAnimalAsync(
            string name,
            Breed breed,
            Food favoriteFood,
            DogAddHierarchicRequest request)
        {
            Dog dog = await _dogService.CreateDogAsync(
                name: name,
                breed: breed,
                favoriteFood: favoriteFood,
                tailLength: request.TailLength);

            return dog;
        }
    }
}
