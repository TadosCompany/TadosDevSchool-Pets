namespace Pets.Controllers.Food
{
    using System.Threading.Tasks;
    using Actions.Add;
    using Actions.Append;
    using Actions.Get;
    using Actions.GetList;
    using Api.Requests.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using AspnetCore.ApiControllers.Abstractions;
    using AspnetCore.ApiControllers.Extensions;

    [ApiController]
    [Route("api/food")]
    public class FoodController : ApiControllerBase
    {
        public FoodController(IAsyncRequestBuilder asyncRequestBuilder)
            : base(asyncRequestBuilder)
        {
        }



        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(FoodGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetList(FoodGetListRequest request)
            => this.RequestAsync<FoodController, FoodGetListRequest, FoodGetListResponse>(request);

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(FoodGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Get(FoodGetRequest request)
            => this.RequestAsync<FoodController, FoodGetRequest, FoodGetResponse>(request);

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(FoodAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Add(FoodAddRequest request)
            => this.RequestAsync<FoodController, FoodAddRequest, FoodAddResponse>(request);

        [HttpPost]
        [Route("append")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Append(FoodAppendRequest request)
            => this.RequestAsync(request);
    }
}