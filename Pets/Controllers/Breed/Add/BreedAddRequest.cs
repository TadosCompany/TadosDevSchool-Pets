namespace Pets.Controllers.Breed.Add
{
    using System.ComponentModel.DataAnnotations;
    using Domain.Enums;

    public class BreedAddRequest
    {
        public AnimalType AnimalType { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
