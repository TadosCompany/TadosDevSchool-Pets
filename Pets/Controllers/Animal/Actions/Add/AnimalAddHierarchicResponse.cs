namespace Pets.Controllers.Animal.Actions.Add
{
    using Api.Requests.Hierarchic.Abstractions;

    public record AnimalAddHierarchicResponse : IHierarchicResponse
    {
        public long Id { get; init; }
    }
}
