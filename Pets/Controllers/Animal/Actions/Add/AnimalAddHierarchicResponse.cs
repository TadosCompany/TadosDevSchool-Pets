namespace Pets.Controllers.Animal.Actions.Add
{
    using Api.Requests.Hierarchic.Abstractions;

    public record AnimalAddHierarchicResponse(

        long Id

    ) : IHierarchicResponse;
}
