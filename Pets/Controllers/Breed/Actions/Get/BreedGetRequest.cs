namespace Pets.Controllers.Breed.Actions.Get
{
    using Api.Requests.Abstractions;

    public record BreedGetRequest : IRequest<BreedGetResponse>
    {
        public long Id { get; init; }
    }
}
