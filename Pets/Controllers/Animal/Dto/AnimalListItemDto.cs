namespace Pets.Controllers.Animal.Dto
{
    using Domain.Enums;

    public record AnimalListItemDto
    {
        public long Id { get; set; }

        public AnimalType Type { get; init; }

        public string Name { get; init; }

        public string BreedName { get; init; }
    }
}
