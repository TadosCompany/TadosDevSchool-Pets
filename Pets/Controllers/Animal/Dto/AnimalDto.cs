namespace Pets.Controllers.Animal.Dto
{
    using System.Collections.Generic;
    using Domain.Enums;
    using Feeding.Dto;

    public record AnimalDto
    {
        public long Id { get; set; }

        public AnimalType Type { get; init; }

        public string Name { get; init; }

        public string BreedName { get; init; }

        public IEnumerable<FeedingDto> Feedings { get; init; }
    }
}
