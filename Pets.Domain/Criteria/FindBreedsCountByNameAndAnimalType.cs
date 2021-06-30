namespace Pets.Domain.Criteria
{
    using Enums;
    using Queries.Abstractions;

    public record FindBreedsCountByNameAndAnimalType(string Name, AnimalType AnimalType) : ICriterion;
}