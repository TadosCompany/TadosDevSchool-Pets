namespace Pets.Controllers.FeedLimit.Actions.Get
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using AutoMapper;
    using Domain.Criteria;
    using Domain.Entities;
    using Dto;
    using Queries.Abstractions;

    public class FeedLimitGetRequestHandler : IAsyncRequestHandler<FeedLimitGetRequest, FeedLimitGetResponse>
    {
        private readonly IAsyncQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;



        public FeedLimitGetRequestHandler(IAsyncQueryBuilder queryBuilder, IMapper mapper)
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<FeedLimitGetResponse> ExecuteAsync(FeedLimitGetRequest request)
        {
            var feedLimit = await _queryBuilder.FindByIdAsync<FeedLimit>(request.Id);

            return new FeedLimitGetResponse(
                FeedLimit: _mapper.Map<FeedLimitDto>(feedLimit));
        }
    }
}