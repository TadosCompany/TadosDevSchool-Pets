namespace Pets.Controllers.Food.GetList
{
    using Models;

    public class FoodGetListRequest
    {
        public AnimalType? AnimalType { get; set; }

        public string Search { get; set; }
    }
}
