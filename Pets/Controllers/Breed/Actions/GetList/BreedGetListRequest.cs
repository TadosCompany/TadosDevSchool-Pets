namespace Pets.Controllers.Breed.Actions.GetList
{
    using Api.Requests.Abstractions;
    using Domain.Enums;

    public class BreedGetListRequest : IRequest<BreedGetListResponse>
    {
        public AnimalType? AnimalType { get; set; }

        public string Search { get; set; }
    }
}
