namespace Pets.Controllers.Animal.Dto
{
    public record HamsterListItemDto : AnimalListItemDto
    {
        public string EyesColor { get; init; }
    }
}
