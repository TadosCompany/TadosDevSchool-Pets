namespace Pets.Controllers.FeedLimit.Actions.Get
{
    using Api.Requests.Abstractions;

    public record FeedLimitGetRequest(long Id) : IRequest<FeedLimitGetResponse>;
}