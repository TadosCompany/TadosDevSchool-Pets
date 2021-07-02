namespace Pets.Controllers.Animal.Actions.SetFavoriteFood
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Queries.Abstractions;

    public class AnimalSetFavoriteFoodRequestHandler : IAsyncRequestHandler<AnimalSetFavoriteFoodRequest>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;



        public AnimalSetFavoriteFoodRequestHandler(IAsyncQueryBuilder asyncQueryBuilder)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
        }



        public async Task ExecuteAsync(AnimalSetFavoriteFoodRequest request)
        {
            var animal = await _asyncQueryBuilder.FindByIdAsync<Animal>(request.AnimalId);

            Food food = null;

            if (request.FoodId.HasValue)
            {
                food = await _asyncQueryBuilder.FindByIdAsync<Food>(request.FoodId.Value);
            }

            animal.SetFavoriteFood(food);
        }
    }
}
