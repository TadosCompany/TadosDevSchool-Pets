namespace Pets.Controllers.Animal.GetList
{
    using Models;

    public class AnimalGetListRequest
    {
        public AnimalType? AnimalType { get; set; }

        public string Search { get; set; }
    }
}
