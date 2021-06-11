namespace Pets.Controllers.Food.GetList
{
    using Models;
    using System.Collections.Generic;

    public class FoodGetListResponse
    {
        public IEnumerable<Food> Foods { get; set; }
    }
}
