namespace Pets.Controllers.Animal.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Common.DataAnnotations.Hierarchy;
    using Domain.Enums;

    [Hierarchy(AnimalType.Hamster)]
    public record HamsterAddHierarchicRequest : AnimalAddHierarchicRequest
    {
        [Required]
        public string EyesColor { get; init; }
    }
}
