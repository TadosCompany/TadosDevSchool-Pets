namespace Pets.Controllers.Animal
{
    using System.Threading.Tasks;
    using Actions.Add;
    using Actions.Feed;
    using Actions.Get;
    using Actions.GetList;
    using Api.Requests.Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using AspnetCore.ApiControllers.Abstractions;
    using AspnetCore.ApiControllers.Extensions;

    [Route("api/animal")]
    public class AnimalController : ApiControllerBase
    {
        public AnimalController(IAsyncRequestBuilder asyncRequestBuilder, IAsyncHierarchicRequestBuilder asyncHierarchicRequestBuilder)
            : base(asyncRequestBuilder, asyncHierarchicRequestBuilder)
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
    }
}
