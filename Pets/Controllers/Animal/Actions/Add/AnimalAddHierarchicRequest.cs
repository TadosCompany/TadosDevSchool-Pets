namespace Pets.Controllers.Animal.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Api.Requests.Hierarchic.Abstractions;
    using Common.DataAnnotations.Hierarchy;
    using Domain.Enums;

    public abstract record AnimalAddHierarchicRequest : IHierarchicRequest<AnimalAddHierarchicResponse>
    {
        [Required]
        [HierarchyDiscriminator]
        public AnimalType Type { get; init; }
        
        public long BreedId { get; init; }

        public long? FavoriteFoodId { get; init; }

        [Required]
        public string Name { get; init; }
    }
}
