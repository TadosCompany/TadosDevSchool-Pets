namespace Pets.Controllers.FeedLimit
{
    using Actions.Add;
    using Actions.GetList;
    using Api.Requests.Abstractions;
    using AspnetCore.ApiControllers.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Api.Requests.Hierarchic.Abstractions;
    using global::Persistence.Transactions.Behaviors;

    [Route("api/feedLimit")]
    public class FeedLimitController : PetsApiControllerBase
    {
        public FeedLimitController(
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
        [ProducesResponseType(typeof(FeedLimitGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetList(FeedLimitGetListRequest request)
            => this.RequestAsync()
                .For<FeedLimitGetListResponse>()
                .With(request);

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(FeedLimitAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Add(FeedLimitAddRequest request)
            => this.RequestAsync()
                .For<FeedLimitAddResponse>()
                .With(request);
    }
}