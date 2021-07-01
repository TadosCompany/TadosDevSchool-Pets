namespace Pets.Controllers.Animal
{
    using System.Threading.Tasks;
    using Actions.Add;
    using Actions.Feed;
    using Actions.Get;
    using Actions.GetList;
    using Actions.SetFavoriteFood;
    using Api.Requests.Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using AspnetCore.ApiControllers.Extensions;
    using global::Persistence.Transactions.Behaviors;

    [Route("api/animal")]
    public class AnimalController : PetsApiControllerBase
    {
        public AnimalController(
            IAsyncRequestBuilder asyncRequestBuilder, 
            IAsyncHierarchicRequestBuilder asyncHierarchicRequestBuilder,
            IExpectCommit commitPerformer)
            : base(
                asyncRequestBuilder, 
                asyncHierarchicRequestBuilder,
                commitPerformer)
        {
        }



        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(AnimalGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetList(AnimalGetListRequest request)
            => this.RequestAsync()
                .For<AnimalGetListResponse>()
                .With(request);

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(AnimalGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Get(AnimalGetRequest request)
            => this.RequestAsync()
                .For<AnimalGetResponse>()
                .With(request);

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(AnimalAddHierarchicResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Add(AnimalAddHierarchicRequest request)
            => this.HierarchicRequestAsync()
                .For<AnimalAddHierarchicResponse>()
                .With(request);

        [HttpPost]
        [Route("feed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Feed(AnimalFeedRequest request)
            => this.RequestAsync(request);

        [HttpPost]
        [Route("setFavoriteFood")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> SetFavoriteFood(AnimalSetFavoriteFoodRequest request)
            => this.RequestAsync(request);
    }
}
