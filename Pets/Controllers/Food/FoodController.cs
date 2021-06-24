namespace Pets.Controllers.Food
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Actions.Add;
    using Actions.Append;
    using Actions.Get;
    using Actions.GetList;
    using AutoMapper;
    using Commands.Abstractions;
    using Domain.Commands.Contexts;
    using Domain.Criteria;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Domain.Entities;
    using Domain.Services.Foods;
    using Dto;
    using Queries.Abstractions;

    [ApiController]
    [Route("api/food")]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IAsyncQueryBuilder _queryBuilder;
        private readonly IAsyncCommandBuilder _commandBuilder;
        private readonly IMapper _mapper;



        public FoodController(
            IFoodService foodService,
            IAsyncQueryBuilder queryBuilder,
            IAsyncCommandBuilder commandBuilder,
            IMapper mapper)
        {
            _foodService = foodService ?? throw new ArgumentNullException(nameof(foodService));
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            _commandBuilder = commandBuilder ?? throw new ArgumentNullException(nameof(commandBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            var response = new FoodGetListResponse
            {
                Foods = _mapper.Map<IEnumerable<FoodDto>>(foods)
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

            var response = new FoodGetResponse
            {
                Food = _mapper.Map<FoodDto>(food),
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

            var response = new FoodAddResponse
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

            var response = new FoodAppendResponse();

            return Json(response);
        }
    }
}