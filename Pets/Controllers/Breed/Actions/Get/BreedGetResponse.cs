namespace Pets.Controllers.Breed.Actions.Get
{
    using Api.Requests.Abstractions;
    using Dto;

    public class BreedGetResponse : IResponse
    {
        public BreedDto Breed { get; set; }
    }
}
