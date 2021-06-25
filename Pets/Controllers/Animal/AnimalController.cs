namespace Pets.Controllers.Animal
{
    using System.Threading.Tasks;
    using Actions.Feed;
    using Actions.Get;
    using Actions.GetList;
    using Api.Requests.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using AspnetCore.ApiControllers.Abstractions;
    using AspnetCore.ApiControllers.Extensions;

    [ApiController]
    [Route("api/animal")]
    public class AnimalController : ApiControllerBase
    {
        public AnimalController(IAsyncRequestBuilder asyncRequestBuilder)
            : base(asyncRequestBuilder)
        {
        }



        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(AnimalGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetList(AnimalGetListRequest request)
            => this.RequestAsync<AnimalController, AnimalGetListRequest, AnimalGetListResponse>(request);

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(AnimalGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Get(AnimalGetRequest request)
            => this.RequestAsync<AnimalController, AnimalGetRequest, AnimalGetResponse>(request);

        //[HttpPost]
        //[Route("add")]
        //[ProducesResponseType(typeof(AnimalAddResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Add(AnimalAddRequest request)
        //{
        //    Animal animal;

        //    Breed breed = await _queryBuilder
        //        .For<Breed>()
        //        .WithAsync(new FindById(request.BreedId));

        //    switch (request.Type)
        //    {
        //        case AnimalType.Cat:
        //            animal = await _catService.CreateCatAsync(
        //                name: request.Name.Trim(),
        //                breed: breed,
        //                weight: request.Weight ?? throw new ArgumentNullException(nameof(request.Weight)));

        //            break;

        //        case AnimalType.Dog:
        //            animal = await _dogService.CreateDogAsync(
        //                name: request.Name.Trim(),
        //                breed: breed,
        //                tailLength: request.TailLength ?? throw new ArgumentNullException(nameof(request.TailLength)));
        //            break;

        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(request.Type), request.Type, "Unknown animal type");
        //    }

        //    var response = new AnimalAddResponse
        //    {
        //        Id = animal.Id,
        //    };

        //    return Json(response);
        //}

        [HttpPost]
        [Route("feed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Feed(AnimalFeedRequest request)
            => this.RequestAsync(request);
    }
}
