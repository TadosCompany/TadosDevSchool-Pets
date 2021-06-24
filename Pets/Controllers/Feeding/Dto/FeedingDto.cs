namespace Pets.Controllers.Feeding.Dto
{
    using System;
    using Food.Dto;

    public record FeedingDto
    {
        public DateTime DateTimeUtc { get; init; }

        public FoodDto Food { get; init; }

        public int Count { get; init; }
    }
}
