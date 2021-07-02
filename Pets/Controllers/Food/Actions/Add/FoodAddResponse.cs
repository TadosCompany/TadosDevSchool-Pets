namespace Pets.Controllers.Food.Actions.Add
{
    using Api.Requests.Abstractions;

    public record FoodAddResponse(

        long Id

    ) : IResponse;
}
