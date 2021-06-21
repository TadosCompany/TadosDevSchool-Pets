namespace Pets.Persistence.Queries.Dto
{
    using Domain.Entities;
    using Domain.ValueObjects;

    internal class FeedingDto
    {
        public long Id { get; set; }

        public string DateTimeUtc { get; set; }

        public int Count { get; set; }


        public Feeding ToEntity(Food food)
        {
            return new Feeding(Id, Helpers.ParseDateTime(DateTimeUtc), food, Count);
        }
    }
}