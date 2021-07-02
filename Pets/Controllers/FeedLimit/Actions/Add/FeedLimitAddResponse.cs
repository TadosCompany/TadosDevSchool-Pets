namespace Pets.Controllers.FeedLimit.Actions.Add
{
    using Api.Requests.Abstractions;

    public record FeedLimitAddResponse(

        long Id

    ) : IResponse;
}
