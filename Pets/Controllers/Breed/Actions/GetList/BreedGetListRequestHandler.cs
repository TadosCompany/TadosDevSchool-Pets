namespace Pets.Controllers.Breed.Actions.GetList
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

    public class BreedGetListRequestHandler : IAsyncRequestHandler<BreedGetListRequest, BreedGetListResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IMapper _mapper;



        public BreedGetListRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IMapper mapper)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<BreedGetListResponse> ExecuteAsync(BreedGetListRequest request)
        {
            List<Breed> breeds = await _asyncQueryBuilder
                .For<List<Breed>>()
                .WithAsync(new FindBySearchAndAnimalType(request.Search, request.AnimalType));

            return new BreedGetListResponse(
                Breeds: _mapper.Map<IEnumerable<BreedDto>>(breeds));
        }
    }
}
