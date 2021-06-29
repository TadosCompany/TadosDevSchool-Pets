namespace NHibernate.Infrastructure.Linq.AsyncQueryable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Linq.AsyncQueryable.Abstractions;
    using NHibernate.Linq;


    public class NHibernateAsyncQueryable<T> : AsyncQueryableBase<T>
    {
        public NHibernateAsyncQueryable(IQueryable<T> query) : base(query)
        {
        }


        public override Task<bool> AnyAsync(
            CancellationToken cancellationToken = default)
            => Query.AnyAsync(cancellationToken);

        public override Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Query.AnyAsync(predicate, cancellationToken);

        public override Task<bool> AllAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Query.AllAsync(predicate, cancellationToken);

        public override Task<int> CountAsync(
            CancellationToken cancellationToken = default)
            => Query.CountAsync(cancellationToken);

        public override Task<int> CountAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Query.CountAsync(predicate, cancellationToken);

        public override Task<int> SumAsync(
            Expression<Func<T, int>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<int?> SumAsync(
            Expression<Func<T, int?>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<long> SumAsync(
            Expression<Func<T, long>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<long?> SumAsync(
            Expression<Func<T, long?>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<float> SumAsync(
            Expression<Func<T, float>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<float?> SumAsync(
            Expression<Func<T, float?>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<double> SumAsync(
            Expression<Func<T, double>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<double?> SumAsync(
            Expression<Func<T, double?>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<decimal> SumAsync(
            Expression<Func<T, decimal>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<decimal?> SumAsync(
            Expression<Func<T, decimal?>> selector,
            CancellationToken cancellationToken = default)
            => Query.SumAsync(selector, cancellationToken);

        public override Task<double> AverageAsync(
            Expression<Func<T, int>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<double?> AverageAsync(
            Expression<Func<T, int?>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<double> AverageAsync(
            Expression<Func<T, long>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<double?> AverageAsync(
            Expression<Func<T, long?>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<float> AverageAsync(
            Expression<Func<T, float>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<float?> AverageAsync(
            Expression<Func<T, float?>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<double> AverageAsync(
            Expression<Func<T, double>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<double?> AverageAsync(
            Expression<Func<T, double?>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<decimal> AverageAsync(
            Expression<Func<T, decimal>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<decimal?> AverageAsync(
            Expression<Func<T, decimal?>> selector,
            CancellationToken cancellationToken = default)
            => Query.AverageAsync(selector, cancellationToken);

        public override Task<T> MinAsync(
            CancellationToken cancellationToken = default)
            => Query.MinAsync(cancellationToken);

        public override Task<TResult> MinAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            CancellationToken cancellationToken = default)
            => Query.MinAsync(selector, cancellationToken);

        public override Task<T> MaxAsync(
            CancellationToken cancellationToken = default)
            => Query.MaxAsync(cancellationToken);

        public override Task<TResult> MaxAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            CancellationToken cancellationToken = default)
            => Query.MaxAsync(selector, cancellationToken);

        public override Task<long> LongCountAsync(
            CancellationToken cancellationToken = default)
            => Query.LongCountAsync(cancellationToken);

        public override Task<long> LongCountAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Query.LongCountAsync(predicate, cancellationToken);

        public override Task<T> FirstAsync(
            CancellationToken cancellationToken = default)
            => Query.FirstAsync(cancellationToken);

        public override Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Query.FirstAsync(predicate, cancellationToken);

        public override Task<T> SingleAsync(
            CancellationToken cancellationToken = default)
            => Query.SingleAsync(cancellationToken);

        public override Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Query.SingleAsync(predicate, cancellationToken);

        public override Task<T> SingleOrDefaultAsync(
            CancellationToken cancellationToken = default)
            => Query.SingleOrDefaultAsync(cancellationToken);

        public override Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Query.SingleOrDefaultAsync(predicate, cancellationToken);

        public override Task<T> FirstOrDefaultAsync(
            CancellationToken cancellationToken = default)
            => Query.FirstOrDefaultAsync(cancellationToken);

        public override Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Query.FirstOrDefaultAsync(predicate, cancellationToken);

        public override Task<List<T>> ToListAsync(
            CancellationToken cancellationToken = default)
            => Query.ToListAsync(cancellationToken);
    }
}