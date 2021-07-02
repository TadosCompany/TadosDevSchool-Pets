namespace Pets.Controllers.Breed.Actions.Get
{
    using Api.Requests.Abstractions;
    using Dto;

    public record BreedGetResponse(

        BreedDto Breed

    ) : IResponse;
}
