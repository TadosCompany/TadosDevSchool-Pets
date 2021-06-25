namespace Pets.Controllers.Breed
{
    using Actions.Add;
    using Actions.Get;
    using Actions.GetList;
    using Api.Requests.Abstractions;
    using AspnetCore.ApiControllers.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Api.Requests.Hierarchic.Abstractions;
    using global::Persistence.Transactions.Behaviors;

    [Route("api/breed")]
    public class BreedController : PetsApiControllerBase
    {
        public BreedController(
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
        [ProducesResponseType(typeof(BreedGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetList(BreedGetListRequest request)
            => this.RequestAsync()
                .For<BreedGetListResponse>()
                .With(request);

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(BreedGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Get(BreedGetRequest request)
            => this.RequestAsync()
                .For<BreedGetResponse>()
                .With(request);

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(BreedAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Add(BreedAddRequest request)
            => this.RequestAsync()
                .For<BreedAddResponse>()
                .With(request);
    }
}