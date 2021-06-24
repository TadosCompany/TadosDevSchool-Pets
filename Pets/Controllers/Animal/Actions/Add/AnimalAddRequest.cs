namespace Pets.Controllers.Animal.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Domain.Enums;

    public class AnimalAddRequest
    {
        [Required]
        public AnimalType Type { get; set; }
        
        public long BreedId { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Range(0, 100000)]
        public decimal? Weight { get; set; }
        
        [Range(0, 100000)]
        public decimal? TailLength { get; set; }
    }
}
