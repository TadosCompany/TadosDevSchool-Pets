namespace Pets.Controllers.Food.Actions.GetList
{
    using System.Collections.Generic;
    using Dto;

    public class FoodGetListResponse
    {
        public IEnumerable<FoodDto> Foods { get; set; }
    }
}
