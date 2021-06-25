namespace Pets.Controllers.Animal.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Common.DataAnnotations.Hierarchy;
    using Domain.Enums;

    [Hierarchy(AnimalType.Dog)]
    public record DogAddHierarchicRequest : AnimalAddHierarchicRequest
    {
        [Range(0, 100000)]
        public decimal TailLength { get; init; }
    }
}
