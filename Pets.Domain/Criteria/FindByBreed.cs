namespace Pets.Domain.Criteria
{
    using Entities;
    using Queries.Abstractions;

    public record FindByBreed(Breed Breed) : ICriterion;
}