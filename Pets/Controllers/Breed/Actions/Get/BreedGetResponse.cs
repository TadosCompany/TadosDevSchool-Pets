namespace Pets.Controllers.Breed.Actions.Get
{
    using Api.Requests.Abstractions;
    using Dto;

    public record BreedGetResponse : IResponse
    {
        public BreedDto Breed { get; init; }
    }
}
