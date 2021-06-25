namespace Pets.Controllers.Breed.Actions.Add
{
    using Api.Requests.Abstractions;

    public record BreedAddResponse : IResponse
    {
        public long Id { get; init; }
    }
}
