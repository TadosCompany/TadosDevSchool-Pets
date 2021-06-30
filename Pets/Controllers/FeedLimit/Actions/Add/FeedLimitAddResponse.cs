namespace Pets.Controllers.FeedLimit.Actions.Add
{
    using Api.Requests.Abstractions;

    public record FeedLimitAddResponse : IResponse
    {
        public long Id { get; init; }
    }
}
