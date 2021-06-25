namespace Pets.Controllers.Animal.Dto
{
    public record DogListItemDto : AnimalListItemDto
    {
        public decimal TailLength { get; init; }
    }
}
