namespace Pets.Controllers.Animal.Actions.Feed
{
    using System.ComponentModel.DataAnnotations;
    using Api.Requests.Abstractions;

    public record AnimalFeedRequest : IRequest
    {
        public long AnimalId { get; init; }

        public long FoodId { get; init; }

        [Range(1, int.MaxValue)]
        public int Count { get; init; }
    }
}
