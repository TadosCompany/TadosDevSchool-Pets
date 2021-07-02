namespace Pets.Controllers.Animal.Actions.Get
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using AutoMapper;
    using Domain.Criteria;
    using Domain.Entities;
    using Dto;
    using Queries.Abstractions;

    public class AnimalGetRequestHandler : IAsyncRequestHandler<AnimalGetRequest, AnimalGetResponse>
    {
        private readonly IAsyncQueryBuilder _asyncQueryBuilder;
        private readonly IMapper _mapper;



        public AnimalGetRequestHandler(IAsyncQueryBuilder asyncQueryBuilder, IMapper mapper)
        {
            _asyncQueryBuilder = asyncQueryBuilder ?? throw new ArgumentNullException(nameof(asyncQueryBuilder));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<AnimalGetResponse> ExecuteAsync(AnimalGetRequest request)
        {
            var animal = await _asyncQueryBuilder.FindByIdAsync<Animal>(request.Id);

            return new AnimalGetResponse(
                Animal: _mapper.Map<AnimalDto>(animal));
        }
    }
}
