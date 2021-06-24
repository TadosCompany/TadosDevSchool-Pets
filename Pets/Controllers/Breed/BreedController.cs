namespace Pets.Controllers.Breed
{
    using System;
    using System.Threading.Tasks;
    using Actions.Add;
    using Actions.Get;
    using Actions.GetList;
    using Api.Requests.Abstractions;
    using AspnetCore.ApiControllers.Abstractions;
    using AspnetCore.ApiControllers.Extensions;
    using AutoMapper;
    using Domain.Criteria;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Domain.Entities;
    using Domain.Services.Breeds;
    using Dto;
    using Queries.Abstractions;

    [ApiController]
    [Route("api/breed")]
    public class BreedController : ApiControllerBase
    {
        private readonly IBreedService _breedService;
        private readonly IAsyncQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;



        public BreedController(
            IAsyncRequestBuilder asyncRequestBuilder,
            IBreedService breedService,
            IAsyncQueryBuilder queryBuilder,
            IMapper mapper)
            : base(asyncRequestBuilder)
        {
            _breedService = breedService ?? throw new ArgumentNullException(nameof(breedService));
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        public async Task<IActionResult> Get(BreedGetRequest request)
        {
            Breed breed = await _queryBuilder
                .For<Breed>()
                .WithAsync(new FindById(request.Id));

            var response = new BreedGetResponse
            {
                Breed = _mapper.Map<BreedDto>(breed)
            };

            return Json(response);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(BreedAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(BreedAddRequest request)
        {
            Breed breed = await _breedService.CreateBreedAsync(
                animalType: request.AnimalType,
                name: request.Name.Trim()
            );

            var response = new BreedAddResponse
            {
                Id = breed.Id,
            };

            return Json(response);
        }
    }
}