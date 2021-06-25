namespace Pets.Controllers.Food
{
    using System.Threading.Tasks;
    using Actions.Add;
    using Actions.Append;
    using Actions.Get;
    using Actions.GetList;
    using Api.Requests.Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using AspnetCore.ApiControllers.Abstractions;
    using AspnetCore.ApiControllers.Extensions;

    [Route("api/food")]
    public class FoodController : ApiControllerBase
    {
        public FoodController(
            IAsyncRequestBuilder asyncRequestBuilder, 
            IAsyncHierarchicRequestBuilder asyncHierarchicRequestBuilder)
            : base(
                asyncRequestBuilder, 
                asyncHierarchicRequestBuilder)
        {
        }



        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(FoodGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetList(FoodGetListRequest request)
            => this.RequestAsync()
                .For<FoodGetListResponse>()
                .With(request);

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(FoodGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Get(FoodGetRequest request)
            => this.RequestAsync()
                .For<FoodGetResponse>()
                .With(request);

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(FoodAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Add(FoodAddRequest request)
            => this.RequestAsync()
                .For<FoodAddResponse>()
                .With(request);

        [HttpPost]
        [Route("append")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Append(FoodAppendRequest request)
            => this.RequestAsync(request);
    }
}