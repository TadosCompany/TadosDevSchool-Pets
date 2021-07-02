namespace Pets.Controllers.Animal.Actions.GetList
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

    public class AnimalGetListRequestHandler : IAsyncRequestHandler<AnimalGetListRequest, AnimalGetListResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IMapper _mapper;



        public AnimalGetListRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IMapper mapper)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<AnimalGetListResponse> ExecuteAsync(AnimalGetListRequest request)
        {
            List<Animal> animals = await _asyncQueryBuilder
                .For<List<Animal>>()
                .WithAsync(new FindBySearchAndAnimalType(request.Search, request.AnimalType));

            return new AnimalGetListResponse(
                Animals: _mapper.Map<IEnumerable<AnimalListItemDto>>(animals));
        }
    }
}
