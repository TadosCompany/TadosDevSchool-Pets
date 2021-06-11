namespace Pets.Models
{
    using System;


    public class Feeding
    {
        public long Id { get; set; }

        public DateTime DateTimeUtc { get; set; }

        public Food Food { get; set; }

        public int Count { get; set; }
    }
}