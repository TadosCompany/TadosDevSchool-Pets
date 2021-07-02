namespace Pets.Controllers.FeedLimit.Actions.Edit
{
    using System.ComponentModel.DataAnnotations;
    using Api.Requests.Abstractions;

    public record FeedLimitEditRequest : IRequest
    {
        public long Id { get; init; }

        [Range(1, int.MaxValue)]
        public int MaxPerDay { get; init; }
    }
}