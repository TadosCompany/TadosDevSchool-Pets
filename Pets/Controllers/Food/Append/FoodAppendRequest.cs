namespace Pets.Controllers.Food.Append
{
    using System.ComponentModel.DataAnnotations;

    public class FoodAppendRequest
    {
        public long Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Count { get; set; }
    }
}
