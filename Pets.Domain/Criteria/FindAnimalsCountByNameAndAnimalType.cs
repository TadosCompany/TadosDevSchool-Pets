namespace Pets.Domain.Criteria
{
    using Enums;
    using Queries.Abstractions;

    public class FindAnimalsCountByNameAndAnimalType : ICriterion
    {
        public FindAnimalsCountByNameAndAnimalType(string name, AnimalType animalType)
        {
            Name = name;
            AnimalType = animalType;
        }


        public string Name { get; }

        public AnimalType AnimalType { get; }
    }
}