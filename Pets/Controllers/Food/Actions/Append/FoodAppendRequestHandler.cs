namespace Pets.Controllers.Food.Actions.Append
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using Commands.Abstractions;
    using Domain.Commands.Contexts;
    using Domain.Criteria;
    using Domain.Entities;
    using Queries.Abstractions;

    public class FoodAppendRequestHandler : IAsyncRequestHandler<FoodAppendRequest>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IAsyncCommandBuilder _asyncCommandBuilder;



        public FoodAppendRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IAsyncCommandBuilder asyncCommandBuilder)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _asyncCommandBuilder = asyncCommandBuilder ?? throw new ArgumentNullException(nameof(asyncCommandBuilder));
        }



        public async Task ExecuteAsync(FoodAppendRequest request)
        {
            Food food = await _asyncQueryBuilder
                .For<Food>()
                .WithAsync(new FindById(request.Id));

            food.Increase(request.Count);

            await _asyncCommandBuilder.ExecuteAsync(new UpdateFoodCommandContext(food));
        }
    }
}
