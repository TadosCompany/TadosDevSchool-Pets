namespace Pets.Controllers.Food.Actions.Get
{
    using Api.Requests.Abstractions;
    using Dto;

    public record FoodGetResponse(

        FoodDto Food

    ) : IResponse;
}
