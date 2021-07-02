namespace Pets.Controllers.Animal.Dto
{
    using System.Collections.Generic;
    using Domain.Enums;
    using Feeding.Dto;
    using Food.Dto;

    public record AnimalDto
    {
        public long Id { get; init; }

        public AnimalType Type { get; init; }

        public string Name { get; init; }

        public string BreedName { get; init; }

        public FoodDto FavoriteFood { get; init; }

        public IEnumerable<FeedingDto> Feedings { get; init; }
    }
}
