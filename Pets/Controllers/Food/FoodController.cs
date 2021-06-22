namespace Pets.Controllers.Food
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Add;
    using Append;
    using Commands.Abstractions;
    using Domain.Commands.Contexts;
    using Domain.Criteria;
    using Get;
    using GetList;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Domain.Entities;
    using Domain.Services.Foods;
    using Queries.Abstractions;


    [ApiController]
    [Route("api/food")]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IAsyncQueryBuilder _queryBuilder;
        private readonly IAsyncCommandBuilder _commandBuilder;


        public FoodController(
            IFoodService foodService,
            IAsyncQueryBuilder queryBuilder,
            IAsyncCommandBuilder commandBuilder)
        {
            _foodService = foodService ?? throw new ArgumentNullException(nameof(foodService));
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            _commandBuilder = commandBuilder ?? throw new ArgumentNullException(nameof(commandBuilder));
        }


        [HttpPost]
        [Route("getList")]
        [ProducesResponseType(typeof(FoodGetListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList(FoodGetListRequest request)
        {
            List<Food> foods = await _queryBuilder
                .For<List<Food>>()
                .WithAsync(new FindBySearchAndAnimalType(request.Search, request.AnimalType));

            FoodGetListResponse response = new FoodGetListResponse()
            {
                Foods = foods,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("get")]
        [ProducesResponseType(typeof(FoodGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(FoodGetRequest request)
        {
            Food food = await _queryBuilder
                .For<Food>()
                .WithAsync(new FindById(request.Id));

            FoodGetResponse response = new FoodGetResponse()
            {
                Food = food,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(FoodAddResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(FoodAddRequest request)
        {
            Food food = await _foodService.CreateFoodAsync(
                animalType: request.AnimalType,
                name: request.Name.Trim()
            );

            FoodAddResponse response = new FoodAddResponse()
            {
                Id = food.Id,
            };

            return Json(response);
        }

        [HttpPost]
        [Route("append")]
        [ProducesResponseType(typeof(FoodAppendResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Append(FoodAppendRequest request)
        {
            Food food = await _queryBuilder
                .For<Food>()
                .WithAsync(new FindById(request.Id));

            food.Increase(request.Count);

            await _commandBuilder.ExecuteAsync(new UpdateFoodCommandContext(food));

            FoodAppendResponse response = new FoodAppendResponse();

            return Json(response);
        }
    }
}