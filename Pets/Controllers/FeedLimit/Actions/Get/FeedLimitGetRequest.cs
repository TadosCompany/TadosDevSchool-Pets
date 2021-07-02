namespace Pets.Controllers.FeedLimit.Actions.Get
{
    using Api.Requests.Abstractions;

    public record FeedLimitGetRequest : IRequest<FeedLimitGetResponse>
    {
        public long Id { get; init; }
    }
}