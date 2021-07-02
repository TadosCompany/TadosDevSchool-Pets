namespace Pets.Controllers.Food.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using Domain.Entities;
    using Domain.Services.Foods;

    public class FoodAddRequestHandler : IAsyncRequestHandler<FoodAddRequest, FoodAddResponse>
    {
        private readonly IFoodService _foodService;



        public FoodAddRequestHandler(IFoodService foodService)
        {
            _foodService = foodService ?? throw new ArgumentNullException(nameof(foodService));
        }



        public async Task<FoodAddResponse> ExecuteAsync(FoodAddRequest request)
        {
            Food food = await _foodService.CreateFoodAsync(
                animalType: request.AnimalType,
                name: request.Name.Trim()
            );

            return new FoodAddResponse(
                Id: food.Id);
        }
    }
}
