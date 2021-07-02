namespace Pets.Controllers.Animal.Actions.Feed
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Domain.Services.Feedings;
    using Queries.Abstractions;

    public class AnimalFeedRequestHandler : IAsyncRequestHandler<AnimalFeedRequest>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IFeedingService _feedingService;



        public AnimalFeedRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IFeedingService feedingService)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _feedingService = feedingService ?? throw new ArgumentNullException(nameof(feedingService));
        }



        public async Task ExecuteAsync(AnimalFeedRequest request)
        {
            var animal = await _asyncQueryBuilder.FindByIdAsync<Animal>(request.AnimalId);

            var food = await _asyncQueryBuilder.FindByIdAsync<Food>(request.FoodId);

            await _feedingService.FeedAsync(animal, food, request.Count);
        }
    }
}
