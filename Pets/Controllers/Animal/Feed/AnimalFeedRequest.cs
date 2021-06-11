namespace Pets.Controllers.Animal.Feed
{
    using System.ComponentModel.DataAnnotations;

    public class AnimalFeedRequest
    {
        public long AnimalId { get; set; }

        public long FoodId { get; set; }

        [Range(1, int.MaxValue)]
        public int Count { get; set; }
    }
}
