namespace Pets.Controllers.Food.Actions.Get
{
    using Api.Requests.Abstractions;

    public record FoodGetRequest : IRequest<FoodGetResponse>
    {
        public long Id { get; init; }
    }
}
