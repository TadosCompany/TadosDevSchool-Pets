namespace Linq.AsyncQueryable.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;


    public abstract class AsyncQueryableBase<T> : IAsyncQueryable<T>
    {
        protected AsyncQueryableBase(IQueryable<T> query)
        {
            Query = query ?? throw new ArgumentNullException(nameof(query));
        }


        protected IQueryable<T> Query { get; }


        public virtual Task<bool> AnyAsync(
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> AllAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> CountAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> SumAsync(
            Expression<Func<T, int>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int?> SumAsync(
            Expression<Func<T, int?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> SumAsync(
            Expression<Func<T, long>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long?> SumAsync(
            Expression<Func<T, long?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<float> SumAsync(
            Expression<Func<T, float>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<float?> SumAsync(
            Expression<Func<T, float?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<double> SumAsync(
            Expression<Func<T, double>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<double?> SumAsync(
            Expression<Func<T, double?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<decimal> SumAsync(
            Expression<Func<T, decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<decimal?> SumAsync(
            Expression<Func<T, decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<double> AverageAsync(
            Expression<Func<T, int>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<double?> AverageAsync(
            Expression<Func<T, int?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<double> AverageAsync(
            Expression<Func<T, long>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<double?> AverageAsync(
            Expression<Func<T, long?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<float> AverageAsync(
            Expression<Func<T, float>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<float?> AverageAsync(
            Expression<Func<T, float?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<double> AverageAsync(
            Expression<Func<T, double>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<double?> AverageAsync(
            Expression<Func<T, double?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<decimal> AverageAsync(
            Expression<Func<T, decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<decimal?> AverageAsync(
            Expression<Func<T, decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> MinAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TResult> MinAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> MaxAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TResult> MaxAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> LongCountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> LongCountAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> FirstAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> SingleAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> SingleOrDefaultAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> ToListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}