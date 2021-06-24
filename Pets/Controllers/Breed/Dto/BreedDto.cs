namespace Pets.Controllers.Breed.Dto
{
    using Domain.Enums;

    public record BreedDto
    {
        public long Id { get; init; }

        public AnimalType AnimalType { get; init; }

        public string Name { get; init; }
    }
}
