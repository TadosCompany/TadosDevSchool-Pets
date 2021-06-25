namespace Pets.Controllers.Food.Actions.GetList
{
    using Api.Requests.Abstractions;
    using Domain.Enums;

    public record FoodGetListRequest : IRequest<FoodGetListResponse>
    {
        public AnimalType? AnimalType { get; init; }

        public string Search { get; init; }
    }
}
