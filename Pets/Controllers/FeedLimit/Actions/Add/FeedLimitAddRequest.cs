namespace Pets.Controllers.FeedLimit.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Api.Requests.Abstractions;

    public record FeedLimitAddRequest : IRequest<FeedLimitAddResponse>
    {
        public long BreedId { get; init; }

        [Range(1, int.MaxValue)]
        public int MaxPerDay { get; init; }
    }
}
