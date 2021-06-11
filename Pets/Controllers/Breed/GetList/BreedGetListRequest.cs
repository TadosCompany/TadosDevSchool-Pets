namespace Pets.Controllers.Breed.GetList
{
    using Models;

    public class BreedGetListRequest
    {
        public AnimalType? AnimalType { get; set; }

        public string Search { get; set; }
    }
}
