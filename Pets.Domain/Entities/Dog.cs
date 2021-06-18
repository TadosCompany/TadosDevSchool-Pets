namespace Pets.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using ValueObjects;

    public class Dog : Animal
    {
        protected internal Dog(string name, Breed breed, decimal tailLength)
            : base(AnimalType.Dog, name, breed)
        {
            if (tailLength < 0) 
                throw new ArgumentOutOfRangeException(nameof(tailLength));

            TailLength = tailLength;
        }

        public Dog(long id, string name, Breed breed, decimal tailLength, IEnumerable<Feeding> feedings)
            : base(id, AnimalType.Dog, name, breed, feedings)
        {
            if (tailLength < 0)
                throw new ArgumentOutOfRangeException(nameof(tailLength));

            TailLength = tailLength;
        }



        public decimal TailLength { get; init; }
    }
}
