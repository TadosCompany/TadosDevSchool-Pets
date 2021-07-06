namespace Pets.Controllers.Animal.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Services.Animals.Hamsters;
    using Queries.Abstractions;

    public class HamsterAddHierarchicRequestHandler : AnimalAddHierarchicRequestHandler<HamsterAddHierarchicRequest>
    {
        private readonly IHamsterService _hamsterService;



        public HamsterAddHierarchicRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IHamsterService hamsterService)
            : base(asyncQueryBuilder)
        {
            _hamsterService = hamsterService ?? throw new ArgumentNullException(nameof(hamsterService));
        }



        protected override async Task<Animal> CreateAnimalAsync(
            string name, 
            Breed breed, 
            Food favoriteFood, 
            HamsterAddHierarchicRequest request)
        {
            Hamster hamster = await _hamsterService.CreateHamsterAsync(
                name: name,
                breed: breed,
                favoriteFood: favoriteFood,
                eyesColor: request.EyesColor);

            return hamster;
        }
    }
}
