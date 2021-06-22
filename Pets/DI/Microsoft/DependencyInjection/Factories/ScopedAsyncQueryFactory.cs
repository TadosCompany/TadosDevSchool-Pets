namespace Pets.DI.Microsoft.DependencyInjection.Factories
{
    using System;
    using global::Microsoft.Extensions.DependencyInjection;
    using Queries.Abstractions;


    public class ScopedAsyncQueryFactory : IAsyncQueryFactory
    {
        private readonly IServiceProvider _serviceProvider;


        public ScopedAsyncQueryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public IAsyncQuery<TCriterion, TResult> Create<TCriterion, TResult>() where TCriterion : ICriterion
        {
            return _serviceProvider.GetService<IAsyncQuery<TCriterion, TResult>>();
        }
    }
}