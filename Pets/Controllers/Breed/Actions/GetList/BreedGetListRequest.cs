namespace Pets.Controllers.Breed.Actions.GetList
{
    using Api.Requests.Abstractions;
    using Domain.Enums;

    public record BreedGetListRequest : IRequest<BreedGetListResponse>
    {
        public AnimalType? AnimalType { get; init; }

        public string Search { get; init; }
    }
}
