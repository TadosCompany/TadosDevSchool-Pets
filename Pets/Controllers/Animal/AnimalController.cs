namespace Pets.Controllers.Animal
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Add;
    using Domain.Criteria;
    using Feed;
    using Get;
    using GetList;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Domain.Enums;
    using Domain.Entities;
    using Domain.Services.Animals.Cats;
    using Domain.Services.Animals.Dogs;
    using Domain.Services.Feedings;
    using Queries.Abstractions;


    [ApiController]
    [Route("api/animal")]
    public class AnimalController : Controller
    {
        private readonly ICatService _catService;
        private readonly IDogService _dogService;
        private readonly IFeedingService _feedingService;
        private readonly IAsyncQueryBuilder _queryBuilder;


        public AnimalController(
            ICatService catService,
            IDogService dogService,
            IFeedingService feedingService,
            IAsyncQueryBuilder queryBuilder)
        {
            _catService = catService ?? throw new ArgumentNullException(nameof(catService));
            _dogService = dogService ?? throw new ArgumentNullException(nameof(dogService));
            _feedingService = feedingService ?? throw new ArgumentNullException(nameof(feedingService));
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }


        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(AnimalGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList(AnimalGetListRequest request)
        {
            List<Animal> animals = await _queryBuilder
                .For<List<Animal>>()
                .WithAsync(new FindBySearchAndAnimalType(
                    request.Search,
                    request.AnimalType));

            AnimalGetListResponse response = new AnimalGetListResponse()
            {
                Animals = animals,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(AnimalGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(AnimalGetRequest request)
        {
            Animal animal = await _queryBuilder
                .For<Animal>()
                .WithAsync(new FindById(request.Id));

            AnimalGetResponse response = new AnimalGetResponse()
            {
                Animal = animal,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(AnimalAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AnimalAddRequest request)
        {
            Animal animal;

            Breed breed = await _queryBuilder
                .For<Breed>()
                .WithAsync(new FindById(request.BreedId));

            switch (request.Type)
            {
                case AnimalType.Cat:
                    animal = await _catService.CreateCatAsync(
                        name: request.Name.Trim(),
                        breed: breed,
                        weight: request.Weight ?? throw new ArgumentNullException(nameof(request.Weight)));

                    break;

                case AnimalType.Dog:
                    animal = await _dogService.CreateDogAsync(
                        name: request.Name.Trim(),
                        breed: breed,
                        tailLength: request.TailLength ?? throw new ArgumentNullException(nameof(request.TailLength)));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(request.Type), request.Type, "Unknown animal type");
            }

            AnimalAddResponse response = new AnimalAddResponse()
            {
                Id = animal.Id,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("feed")]
        [ProducesResponseType(typeof(AnimalFeedResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Feed(AnimalFeedRequest request)
        {
            Animal animal = await _queryBuilder
                .For<Animal>()
                .WithAsync(new FindById(request.AnimalId));

            Food food = await _queryBuilder
                .For<Food>()
                .WithAsync(new FindById(request.FoodId));

            await _feedingService.FeedAsync(animal, food, request.Count);

            AnimalFeedResponse response = new AnimalFeedResponse();

            return Json(response);
        }
    }
}
