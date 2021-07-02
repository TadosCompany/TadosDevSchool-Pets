namespace Pets.Controllers.Breed.Actions.Get
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using AutoMapper;
    using Domain.Criteria;
    using Domain.Entities;
    using Dto;
    using Queries.Abstractions;

    public class BreedGetRequestHandler : IAsyncRequestHandler<BreedGetRequest, BreedGetResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IMapper _mapper;



        public BreedGetRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IMapper mapper)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<BreedGetResponse> ExecuteAsync(BreedGetRequest request)
        {
            var breed = await _asyncQueryBuilder.FindByIdAsync<Breed>(request.Id);

            return new BreedGetResponse(
                Breed: _mapper.Map<BreedDto>(breed));
        }
    }
}
