namespace Pets.Controllers.Food.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Domain.Enums;

    public class FoodAddRequest
    {
        [Required]
        public AnimalType AnimalType { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
