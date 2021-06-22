namespace Pets.Domain.Criteria
{
    using Enums;
    using Queries.Abstractions;

    public class FindFoodsCountByNameAndAnimalType : ICriterion
    {
        public FindFoodsCountByNameAndAnimalType(string name, AnimalType animalType)
        {
            Name = name;
            AnimalType = animalType;
        }


        public string Name { get; }

        public AnimalType AnimalType { get; }
    }
}