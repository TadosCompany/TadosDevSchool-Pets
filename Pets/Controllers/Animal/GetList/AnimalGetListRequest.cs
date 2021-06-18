namespace Pets.Controllers.Animal.GetList
{
    using Domain.Enums;

    public class AnimalGetListRequest
    {
        public AnimalType? AnimalType { get; set; }

        public string Search { get; set; }
    }
}
