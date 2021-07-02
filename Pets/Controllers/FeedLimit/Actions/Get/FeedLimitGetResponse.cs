namespace Pets.Controllers.FeedLimit.Actions.Get
{
    using Api.Requests.Abstractions;
    using Dto;

    public record FeedLimitGetResponse(
        
        FeedLimitDto FeedLimit

    ) : IResponse;
}