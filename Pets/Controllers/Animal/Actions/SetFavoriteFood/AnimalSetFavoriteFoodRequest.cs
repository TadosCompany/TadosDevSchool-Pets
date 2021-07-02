namespace Pets.Controllers.Animal.Actions.SetFavoriteFood
{
    using Api.Requests.Abstractions;

    public record AnimalSetFavoriteFoodRequest : IRequest
    {
        public long AnimalId { get; init; }

        public long? FoodId { get; init; }
    }
}
