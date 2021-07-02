namespace Pets.Controllers.FeedLimit.Dto
{
    using Breed.Dto;

    public record FeedLimitDto
    {
        public long Id { get; init; }

        public BreedDto Breed { get; init; }

        public int MaxPerDay { get; init; }
    }
}
