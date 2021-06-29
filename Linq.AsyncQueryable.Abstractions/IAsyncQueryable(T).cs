namespace Linq.AsyncQueryable.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;


    public interface IAsyncQueryable<T>
    {
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<int> SumAsync(Expression<Func<T, int>> selector, CancellationToken cancellationToken = default);

        Task<int?> SumAsync(Expression<Func<T, int?>> selector, CancellationToken cancellationToken = default);

        Task<long> SumAsync(Expression<Func<T, long>> selector, CancellationToken cancellationToken = default);

        Task<long?> SumAsync(Expression<Func<T, long?>> selector, CancellationToken cancellationToken = default);

        Task<float> SumAsync(Expression<Func<T, float>> selector, CancellationToken cancellationToken = default);

        Task<float?> SumAsync(Expression<Func<T, float?>> selector, CancellationToken cancellationToken = default);

        Task<double> SumAsync(Expression<Func<T, double>> selector, CancellationToken cancellationToken = default);

        Task<double?> SumAsync(Expression<Func<T, double?>> selector, CancellationToken cancellationToken = default);

        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, CancellationToken cancellationToken = default);

        Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector, CancellationToken cancellationToken = default);

        Task<double> AverageAsync(Expression<Func<T, int>> selector, CancellationToken cancellationToken = default);

        Task<double?> AverageAsync(Expression<Func<T, int?>> selector, CancellationToken cancellationToken = default);

        Task<double> AverageAsync(Expression<Func<T, long>> selector, CancellationToken cancellationToken = default);

        Task<double?> AverageAsync(Expression<Func<T, long?>> selector, CancellationToken cancellationToken = default);

        Task<float> AverageAsync(Expression<Func<T, float>> selector, CancellationToken cancellationToken = default);

        Task<float?> AverageAsync(Expression<Func<T, float?>> selector, CancellationToken cancellationToken = default);

        Task<double> AverageAsync(Expression<Func<T, double>> selector, CancellationToken cancellationToken = default);

        Task<double?> AverageAsync(Expression<Func<T, double?>> selector,
            CancellationToken cancellationToken = default);

        Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector,
            CancellationToken cancellationToken = default);

        Task<decimal?> AverageAsync(Expression<Func<T, decimal?>> selector,
            CancellationToken cancellationToken = default);

        Task<T> MinAsync(CancellationToken cancellationToken = default);

        Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector,
            CancellationToken cancellationToken = default);

        Task<T> MaxAsync(CancellationToken cancellationToken = default);

        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector,
            CancellationToken cancellationToken = default);

        Task<long> LongCountAsync(CancellationToken cancellationToken = default);

        Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T> FirstAsync(CancellationToken cancellationToken = default);

        Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T> SingleAsync(CancellationToken cancellationToken = default);

        Task<T> SingleAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T> SingleOrDefaultAsync(CancellationToken cancellationToken = default);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<List<T>> ToListAsync(CancellationToken cancellationToken = default);
    }
}