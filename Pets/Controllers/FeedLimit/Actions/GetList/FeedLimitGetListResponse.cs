namespace Pets.Controllers.FeedLimit.Actions.GetList
{
    using System.Collections.Generic;
    using Api.Requests.Abstractions;
    using Dto;

    public record FeedLimitGetListResponse(

        IEnumerable<FeedLimitDto> FeedLimits

    ) : IResponse;
}
