namespace Pets.Controllers.Breed
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Add;
    using Domain.Criteria;
    using Get;
    using GetList;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Domain.Entities;
    using Domain.Services.Breeds;
    using Queries.Abstractions;


    [ApiController]
    [Route("api/breed")]
    public class BreedController : Controller
    {
        private readonly IBreedService _breedService;
        private readonly IAsyncQueryBuilder _queryBuilder;


        public BreedController(
            IBreedService breedService,
            IAsyncQueryBuilder queryBuilder)
        {
            _breedService = breedService ?? throw new ArgumentNullException(nameof(breedService));
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }


        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(BreedGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList(BreedGetListRequest request)
        {
            List<Breed> breeds = await _queryBuilder
                .For<List<Breed>>()
                .WithAsync(new FindBySearchAndAnimalType(request.Search, request.AnimalType));

            BreedGetListResponse response = new BreedGetListResponse()
            {
                Breeds = breeds,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(BreedGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(BreedGetRequest request)
        {
            Breed breed = await _queryBuilder
                .For<Breed>()
                .WithAsync(new FindById(request.Id));

            BreedGetResponse response = new BreedGetResponse()
            {
                Breed = breed,
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

            BreedAddResponse response = new BreedAddResponse()
            {
                Id = breed.Id,
            };

            return Json(response);
        }
    }
}