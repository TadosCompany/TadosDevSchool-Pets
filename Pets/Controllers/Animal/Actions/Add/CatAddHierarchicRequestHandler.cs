namespace Pets.Controllers.Animal.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Hierarchic.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Domain.Services.Animals.Cats;
    using Queries.Abstractions;

    public class CatAddHierarchicRequestHandler : AsyncHierarchicRequestHandlerBase<CatAddHierarchicRequest, AnimalAddHierarchicResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly ICatService _catService;



        public CatAddHierarchicRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, ICatService catService)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _catService = catService ?? throw new ArgumentNullException(nameof(catService));
        }



        protected override async Task<AnimalAddHierarchicResponse> ExecuteAsync(CatAddHierarchicRequest request)
        {
            Breed breed = await _asyncQueryBuilder
                .For<Breed>()
                .WithAsync(new FindById(request.BreedId));

            Cat cat = await _catService.CreateCatAsync(
                name: request.Name.Trim(),
                breed: breed,
                weight: request.Weight);

            return new AnimalAddHierarchicResponse
            {
                Id = cat.Id
            };
        }
    }
}
