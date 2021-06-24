namespace Pets.Controllers.Breed.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Domain.Enums;

    public class BreedAddRequest
    {
        [Required]
        public AnimalType AnimalType { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
