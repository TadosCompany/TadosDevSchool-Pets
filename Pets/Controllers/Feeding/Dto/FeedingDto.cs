namespace Pets.Controllers.Feeding.Dto
{
    using System;

    public record FeedingDto
    {
        public DateTime DateTimeUtc { get; init; }

        public string FoodName { get; init; }

        public int Count { get; init; }
    }
}
