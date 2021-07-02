namespace Pets.Controllers.Food.Actions.Get
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using AutoMapper;
    using Domain.Criteria;
    using Domain.Entities;
    using Dto;
    using Queries.Abstractions;

    public class FoodGetRequestHandler : IAsyncRequestHandler<FoodGetRequest, FoodGetResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IMapper _mapper;



        public FoodGetRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IMapper mapper)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<FoodGetResponse> ExecuteAsync(FoodGetRequest request)
        {
            var food = await _asyncQueryBuilder.FindByIdAsync<Food>(request.Id);

            return new FoodGetResponse(
                Food: _mapper.Map<FoodDto>(food));
        }
    }
}
