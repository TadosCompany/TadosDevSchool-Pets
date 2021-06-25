namespace Pets.Controllers.Food.Actions.Append
{
    using System.ComponentModel.DataAnnotations;
    using Api.Requests.Abstractions;

    public record FoodAppendRequest : IRequest
    {
        public long Id { get; init; }

        [Range(1, int.MaxValue)]
        public int Count { get; init; }
    }
}
