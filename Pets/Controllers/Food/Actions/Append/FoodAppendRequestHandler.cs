namespace Pets.Controllers.Food.Actions.Append
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Queries.Abstractions;

    public class FoodAppendRequestHandler : IAsyncRequestHandler<FoodAppendRequest>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;



        public FoodAppendRequestHandler(IAsyncQueryBuilder asyncQueryBuilder)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
        }



        public async Task ExecuteAsync(FoodAppendRequest request)
        {
            Food food = await _asyncQueryBuilder
                .For<Food>()
                .WithAsync(new FindById(request.Id));

            food.Increase(request.Count);
        }
    }
}
