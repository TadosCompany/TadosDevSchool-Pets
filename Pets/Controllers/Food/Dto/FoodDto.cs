namespace Pets.Controllers.Food.Dto
{
    using Domain.Enums;

    public record FoodDto
    {
        public long Id { get; init; }

        public AnimalType AnimalType { get; init; }

        public string Name { get; init; }

        public int Count { get; init; }
    }
}
