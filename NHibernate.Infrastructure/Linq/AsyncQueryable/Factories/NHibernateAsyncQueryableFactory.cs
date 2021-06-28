namespace NHibernate.Infrastructure.Linq.AsyncQueryable.Factories
{
    using System.Linq;
    using global::Linq.AsyncQueryable.Abstractions;
    using global::Linq.AsyncQueryable.Abstractions.Factories;


    public class NHibernateAsyncQueryableFactory : IAsyncQueryableFactory
    {
        public IAsyncQueryable<T> CreateFrom<T>(IQueryable<T> query) => new NHibernateAsyncQueryable<T>(query);
    }
}