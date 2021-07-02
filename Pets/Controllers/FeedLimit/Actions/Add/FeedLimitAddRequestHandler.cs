namespace Pets.Controllers.FeedLimit.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Domain.Services.FeedLimits;
    using Queries.Abstractions;

    public class FeedLimitAddRequestHandler : IAsyncRequestHandler<FeedLimitAddRequest, FeedLimitAddResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IFeedLimitService _feedLimitService;



        public FeedLimitAddRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IFeedLimitService feedLimitService)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _feedLimitService = feedLimitService ?? throw new ArgumentNullException(nameof(feedLimitService));
        }



        public async Task<FeedLimitAddResponse> ExecuteAsync(FeedLimitAddRequest request)
        {
            var breed = await _asyncQueryBuilder.FindByIdAsync<Breed>(request.BreedId);

            FeedLimit feedLimit = await _feedLimitService.CreateFeedLimitAsync(breed, request.MaxPerDay);

            return new FeedLimitAddResponse(
                Id: feedLimit.Id);
        }
    }
}
