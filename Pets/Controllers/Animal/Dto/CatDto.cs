namespace Pets.Controllers.Animal.Dto
{
    public record CatDto : AnimalDto
    {
        public decimal Weight { get; init; }
    }
}
