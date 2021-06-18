namespace Pets.Controllers.Food.GetList
{
    using Domain.Enums;

    public class FoodGetListRequest
    {
        public AnimalType? AnimalType { get; set; }

        public string Search { get; set; }
    }
}
