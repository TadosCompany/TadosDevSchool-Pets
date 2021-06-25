namespace Pets.Controllers.Animal.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Common.DataAnnotations.Hierarchy;
    using Domain.Enums;

    [Hierarchy(AnimalType.Cat)]
    public record CatAddHierarchicRequest : AnimalAddHierarchicRequest
    {
        [Range(0, 100000)]
        public decimal Weight { get; init; }
    }
}
