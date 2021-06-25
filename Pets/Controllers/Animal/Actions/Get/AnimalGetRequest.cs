namespace Pets.Controllers.Animal.Actions.Get
{
    using Api.Requests.Abstractions;

    public record AnimalGetRequest : IRequest<AnimalGetResponse>
    {
        public long Id { get; init; }
    }
}
