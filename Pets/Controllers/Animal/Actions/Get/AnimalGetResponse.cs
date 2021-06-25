namespace Pets.Controllers.Animal.Actions.Get
{
    using Api.Requests.Abstractions;
    using Dto;

    public record AnimalGetResponse : IResponse
    {
        public AnimalDto Animal { get; init; }
    }
}
