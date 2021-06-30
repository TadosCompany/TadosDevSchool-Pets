namespace Pets.Controllers.Animal.Dto
{
    public record HamsterDto : AnimalDto
    {
        public string EyesColor { get; init; }
    }
}
