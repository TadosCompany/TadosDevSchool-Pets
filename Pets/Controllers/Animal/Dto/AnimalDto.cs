namespace Pets.Controllers.Animal.Dto
{
    using System.Collections.Generic;
    using Breed.Dto;
    using Domain.Enums;
    using Feeding.Dto;

    public record AnimalDto
    {
        public long Id { get; set; }

        public AnimalType Type { get; init; }

        public string Name { get; init; }

        public BreedDto Breed { get; init; }

        public IEnumerable<FeedingDto> Feedings { get; init; }
    }
}
