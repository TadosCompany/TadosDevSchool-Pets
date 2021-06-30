namespace Pets.Controllers.FeedLimit.Actions.GetList
{
    using System.Collections.Generic;
    using Api.Requests.Abstractions;
    using Dto;

    public record FeedLimitGetListResponse : IResponse
    {
        public IEnumerable<FeedLimitDto> FeedLimits { get; init; }
    }
}
