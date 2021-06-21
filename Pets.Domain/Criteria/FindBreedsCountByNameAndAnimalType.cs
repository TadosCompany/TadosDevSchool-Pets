namespace Pets.Domain.Criteria
{
    using Enums;
    using Queries.Abstractions;

    public class FindBreedsCountByNameAndAnimalType : ICriterion
    {
        public FindBreedsCountByNameAndAnimalType(string name, AnimalType animalType)
        {
            Name = name;
            AnimalType = animalType;
        }


        public string Name { get; }

        public AnimalType AnimalType { get; }
    }
}