namespace Pets.Controllers.Animal.Dto
{
    public record DogDto : AnimalDto
    {
        public decimal TailLength { get; init; }
    }
}
