namespace Pets.Controllers.Food.GetList
{
    using System.Collections.Generic;
    using Domain.Entities;

    public class FoodGetListResponse
    {
        public IEnumerable<Food> Foods { get; set; }
    }
}
