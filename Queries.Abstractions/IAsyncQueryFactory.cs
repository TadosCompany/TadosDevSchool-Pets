namespace Queries.Abstractions
{
    public interface IAsyncQueryFactory
    {
        IAsyncQuery<TCriterion, TResult> Create<TCriterion, TResult>() where TCriterion : ICriterion;
    }
}