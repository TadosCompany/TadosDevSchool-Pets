namespace Pets.Controllers.Food.Actions.GetList
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

    public class FoodGetListRequestHandler : IAsyncRequestHandler<FoodGetListRequest, FoodGetListResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IMapper _mapper;



        public FoodGetListRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IMapper mapper)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<FoodGetListResponse> ExecuteAsync(FoodGetListRequest request)
        {
            List<Food> foods = await _asyncQueryBuilder
                .For<List<Food>>()
                .WithAsync(new FindBySearchAndAnimalType(request.Search, request.AnimalType));

            return new FoodGetListResponse(
                Foods: _mapper.Map<IEnumerable<FoodDto>>(foods));
        }
    }
}
