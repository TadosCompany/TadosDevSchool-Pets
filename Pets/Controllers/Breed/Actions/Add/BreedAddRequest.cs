namespace Pets.Controllers.Breed.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Api.Requests.Abstractions;
    using Domain.Enums;

    public record BreedAddRequest : IRequest<BreedAddResponse>
    {
        [Required]
        public AnimalType AnimalType { get; init; }

        [Required]
        public string Name { get; init; }
    }
}
