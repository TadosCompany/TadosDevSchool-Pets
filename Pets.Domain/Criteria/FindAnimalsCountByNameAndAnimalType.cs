namespace Pets.Domain.Criteria
{
    using Enums;
    using Queries.Abstractions;

    public record FindAnimalsCountByNameAndAnimalType(string Name, AnimalType AnimalType) : ICriterion;
}