namespace Pets.Persistence.Queries.Dto
{
    using System;
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;


    internal class AnimalDto
    {
        public long Id { get; set; }

        public AnimalType Type { get; set; }

        public string Name { get; set; }

        public decimal? Weight { get; set; }

        public decimal? TailLength { get; set; }
        
        public BreedDto Breed { get; set; }


        public Animal ToEntity(Breed breed, IEnumerable<Feeding> feedings)
        {
            switch (Type)
            {
                case AnimalType.Cat:
                    return new Cat(Id, Name, breed, Weight ?? throw new ArgumentException(nameof(Weight)), feedings);
                case AnimalType.Dog:
                    return new Dog(Id, Name, breed, TailLength ?? throw new ArgumentException(nameof(TailLength)), feedings);
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), Type, "Unknown animal type");
            }
        }
    }
}