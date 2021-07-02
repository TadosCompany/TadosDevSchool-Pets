namespace Pets.Controllers.Breed.Actions.Add
{
    using Api.Requests.Abstractions;

    public record BreedAddResponse(

        long Id

    ) : IResponse;
}
