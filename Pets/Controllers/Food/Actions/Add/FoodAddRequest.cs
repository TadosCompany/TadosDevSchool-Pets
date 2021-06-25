namespace Pets.Controllers.Food.Actions.Add
{
    using System.ComponentModel.DataAnnotations;
    using Api.Requests.Abstractions;
    using Domain.Enums;

    public record FoodAddRequest : IRequest<FoodAddResponse>
    {
        [Required]
        public AnimalType AnimalType { get; init; }

        [Required]
        public string Name { get; init; }
    }
}
