namespace Pets.Controllers.FeedLimit.Actions.GetList
{
    using Api.Requests.Abstractions;
    using Domain.Enums;

    public record FeedLimitGetListRequest : IRequest<FeedLimitGetListResponse>
    {
        public AnimalType? AnimalType { get; init; }

        public string Search { get; init; }
    }
}
