namespace Pets.Controllers.Breed.GetList
{
    using Domain.Enums;

    public class BreedGetListRequest
    {
        public AnimalType? AnimalType { get; set; }

        public string Search { get; set; }
    }
}
