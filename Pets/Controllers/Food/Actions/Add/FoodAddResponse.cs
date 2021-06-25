namespace Pets.Controllers.Food.Actions.Add
{
    using Api.Requests.Abstractions;

    public record FoodAddResponse : IResponse
    {
        public long Id { get; init; }
    }
}
