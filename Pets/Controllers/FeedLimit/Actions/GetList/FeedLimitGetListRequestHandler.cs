namespace Pets.Controllers.FeedLimit.Actions.GetList
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using AutoMapper;
    using Domain.Criteria;
    using Domain.Entities;
    using Dto;
    using Queries.Abstractions;

    public class FeedLimitGetListRequestHandler : IAsyncRequestHandler<FeedLimitGetListRequest, FeedLimitGetListResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IMapper _mapper;



        public FeedLimitGetListRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IMapper mapper)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<FeedLimitGetListResponse> ExecuteAsync(FeedLimitGetListRequest request)
        {
            List<FeedLimit> feedLimits = await _asyncQueryBuilder
                .For<List<FeedLimit>>()
                .WithAsync(new FindBySearchAndAnimalType(request.Search, request.AnimalType));

            return new FeedLimitGetListResponse(
                FeedLimits: _mapper.Map<IEnumerable<FeedLimitDto>>(feedLimits));
        }
    }
}
