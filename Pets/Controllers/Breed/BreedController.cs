namespace Pets.Controllers.Breed
{
    using Actions.Add;
    using Actions.Get;
    using Actions.GetList;
    using Api.Requests.Abstractions;
    using AspnetCore.ApiControllers.Abstractions;
    using AspnetCore.ApiControllers.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Api.Requests.Hierarchic.Abstractions;

    [Route("api/breed")]
    public class BreedController : ApiControllerBase
    {
        public BreedController(IAsyncRequestBuilder asyncRequestBuilder, IAsyncHierarchicRequestBuilder asyncHierarchicRequestBuilder)
            : base(asyncRequestBuilder, asyncHierarchicRequestBuilder)
        {
        }



        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(BreedGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetList(BreedGetListRequest request)
            => this.RequestAsync<BreedController, BreedGetListRequest, BreedGetListResponse>(request);

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(BreedGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Get(BreedGetRequest request)
            => this.RequestAsync<BreedController, BreedGetRequest, BreedGetResponse>(request);

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(BreedAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Add(BreedAddRequest request)
            => this.RequestAsync<BreedController, BreedAddRequest, BreedAddResponse>(request);
    }
}