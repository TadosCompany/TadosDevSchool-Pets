namespace Pets.Controllers.Food.Actions.Get
{
    using Api.Requests.Abstractions;
    using Dto;

    public record FoodGetResponse : IResponse
    {
        public FoodDto Food { get; init; }
    }
}
