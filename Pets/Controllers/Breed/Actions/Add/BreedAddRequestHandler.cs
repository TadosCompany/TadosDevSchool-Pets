namespace Pets.Controllers.Breed.Actions.Add
{
    using System;
    using System.Threading.Tasks;
    using Api.Requests.Abstractions;
    using Domain.Entities;
    using Domain.Services.Breeds;

    public class BreedAddRequestHandler : IAsyncRequestHandler<BreedAddRequest, BreedAddResponse>
    {
        private readonly IBreedService _breedService;



        public BreedAddRequestHandler(IBreedService breedService)
        {
            _breedService = breedService ?? throw new ArgumentNullException(nameof(breedService));
        }



        public async Task<BreedAddResponse> ExecuteAsync(BreedAddRequest request)
        {
            Breed breed = await _breedService.CreateBreedAsync(
                animalType: request.AnimalType,
                name: request.Name.Trim()
            );

            return new BreedAddResponse(
                Id: breed.Id);
        }
    }
}
