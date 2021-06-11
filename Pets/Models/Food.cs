namespace Pets.Models
{
    public class Food
    {
        public long Id { get; set; }

        public AnimalType AnimalType { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }
    }
}
