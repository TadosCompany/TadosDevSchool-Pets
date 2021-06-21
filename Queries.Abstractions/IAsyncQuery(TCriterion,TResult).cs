namespace Queries.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;


    public interface IAsyncQuery<in TCriterion, TResult> where TCriterion : ICriterion
    {
        Task<TResult> AskAsync(TCriterion criterion, CancellationToken cancellationToken = default);
    }
}