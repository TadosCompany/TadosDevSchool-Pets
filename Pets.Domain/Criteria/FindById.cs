namespace Pets.Domain.Criteria
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Domain.Abstractions;
    using Queries.Abstractions;

    public record FindById(long Id) : ICriterion;



    public static class FindByIdCriterionExtensions
    {
        public static Task<THasId> FindByIdAsync<THasId>(
            this IAsyncQueryBuilder asyncQueryBuilder,
            long id,
            CancellationToken cancellationToken = default) 
            where THasId : class, IHasId, new()
        {
            return asyncQueryBuilder
                .For<THasId>()
                .WithAsync(new FindById(id), cancellationToken);
        }
    }
}