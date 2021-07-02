namespace Pets.Domain.Criteria
{
    using Enums;
    using Queries.Abstractions;

    public record FindFoodsCountByNameAndAnimalType(string Name, AnimalType AnimalType) : ICriterion;
}