namespace Pets.Controllers.Animal.Actions.Get
{
    using Api.Requests.Abstractions;
    using Dto;

    public record AnimalGetResponse(

        AnimalDto Animal

    ) : IResponse;
}
