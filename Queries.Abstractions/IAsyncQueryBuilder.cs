namespace Queries.Abstractions
{
    public interface IAsyncQueryBuilder
    {
        IAsyncQueryFor<TResult> For<TResult>();
    }
}