namespace Pets.Controllers.Animal.Dto
{
    public record CatListItemDto : AnimalListItemDto
    {
        public decimal Weight { get; init; }
    }
}
