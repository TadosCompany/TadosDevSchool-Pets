namespace Linq.AsyncQueryable.Abstractions.Factories
{
    using System.Linq;


    public interface IAsyncQueryableFactory
    {
        IAsyncQueryable<T> CreateFrom<T>(IQueryable<T> query);
    }
}