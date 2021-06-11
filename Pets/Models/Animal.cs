namespace Pets.Models
{
    using System.Collections.Generic;

    public abstract class Animal
    {
        public long Id { get; set; }

        public AnimalType Type { get; set; }

        public string Name { get; set; }

        public Breed Breed { get; set; }
        
        public IEnumerable<Feeding> Feedings { get; set; }
    }
}
