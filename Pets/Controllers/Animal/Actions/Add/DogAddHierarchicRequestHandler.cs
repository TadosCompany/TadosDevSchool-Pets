namespace Pets.Controllers.Animal.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Hierarchic.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Domain.Services.Animals.Dogs;
    using Queries.Abstractions;

    public class DogAddHierarchicRequestHandler : AsyncHierarchicRequestHandlerBase<DogAddHierarchicRequest, AnimalAddHierarchicResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IDogService _dogService;



        public DogAddHierarchicRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IDogService dogService)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _dogService = dogService ?? throw new ArgumentNullException(nameof(dogService));
        }



        protected override async Task<AnimalAddHierarchicResponse> ExecuteAsync(DogAddHierarchicRequest request)
        {
            Breed breed = await _asyncQueryBuilder
                .For<Breed>()
                .WithAsync(new FindById(request.BreedId));

            Dog dog = await _dogService.CreateDogAsync(
                name: request.Name.Trim(),
                breed: breed,
                tailLength: request.TailLength);

            return new AnimalAddHierarchicResponse
            {
                Id = dog.Id
            };
        }
    }
}
