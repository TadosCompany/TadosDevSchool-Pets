namespace Pets.Controllers.Animal.Actions.GetList
{
    using Api.Requests.Abstractions;
    using Domain.Enums;

    public record AnimalGetListRequest : IRequest<AnimalGetListResponse>
    {
        public AnimalType? AnimalType { get; init; }

        public string Search { get; init; }
    }
}
