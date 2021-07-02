namespace Pets.Domain.Criteria
{
    using Enums;
    using Queries.Abstractions;

    public record FindBySearchAndAnimalType(string Search, AnimalType? AnimalType) : ICriterion;
}