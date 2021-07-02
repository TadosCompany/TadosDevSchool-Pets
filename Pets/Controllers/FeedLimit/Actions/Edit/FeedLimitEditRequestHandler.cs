namespace Pets.Controllers.FeedLimit.Actions.Edit
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Queries.Abstractions;

    public class FeedLimitEditRequestHandler : IAsyncRequestHandler<FeedLimitEditRequest>
    {
        private readonly IAsyncQueryBuilder _queryBuilder;


        public FeedLimitEditRequestHandler(IAsyncQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }
        
        
        public async Task ExecuteAsync(FeedLimitEditRequest request)
        {
            var feedLimit = await _queryBuilder.FindByIdAsync<FeedLimit>(request.Id);

            feedLimit.SetMaxPerDay(request.MaxPerDay);
        }
    }
}