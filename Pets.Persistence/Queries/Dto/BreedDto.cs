namespace Pets.Persistence.Queries.Dto
{
    using Domain.Entities;
    using Domain.Enums;

    internal class BreedDto
    {
        public long Id { get; set; }

        public AnimalType AnimalType { get; set; }

        public string Name { get; set; }


        public Breed ToEntity()
        {
            return new Breed(Id, AnimalType, Name);
        }
    }
}