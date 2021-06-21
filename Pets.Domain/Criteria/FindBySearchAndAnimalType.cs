namespace Pets.Domain.Criteria
{
    using Enums;
    using Queries.Abstractions;


    public class FindBySearchAndAnimalType : ICriterion
    {
        public FindBySearchAndAnimalType(string search, AnimalType? animalType)
        {
            Search = search;
            AnimalType = animalType;
        }


        public string Search { get; }

        public AnimalType? AnimalType { get; }
    }
}