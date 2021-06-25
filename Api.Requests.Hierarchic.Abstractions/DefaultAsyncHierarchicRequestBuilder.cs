namespace Api.Requests.Hierarchic.Abstractions
{
    using System;
    using System.Threading.Tasks;

    public class DefaultAsyncHierarchicRequestBuilder : IAsyncHierarchicRequestBuilder
    {
        private readonly IAsyncHierarchicRequestHandlerFactory _asyncHierarchicRequestHandlerFactory;



        public DefaultAsyncHierarchicRequestBuilder(IAsyncHierarchicRequestHandlerFactory asyncHierarchicRequestHandlerFactory)
        {
            _asyncHierarchicRequestHandlerFactory = asyncHierarchicRequestHandlerFactory ?? throw new ArgumentNullException(nameof(asyncHierarchicRequestHandlerFactory));
        }



        public Task ExecuteAsync<THierarchicRequest>(THierarchicRequest request)
            where THierarchicRequest : IHierarchicRequest
        {
            var requestHandler = (IAsyncWeaklyTypedHierarchicRequestHandler)_asyncHierarchicRequestHandlerFactory.Create(request);

            return requestHandler.ExecuteAsync(request);
        }

        public Task<THierarchicResponse> ExecuteAsync<THierarchicRequest, THierarchicResponse>(THierarchicRequest request)
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            where THierarchicResponse : IHierarchicResponse
        {
            var requestHandler = (IAsyncWeaklyTypedHierarchicRequestHandler<THierarchicResponse>)_asyncHierarchicRequestHandlerFactory.Create<THierarchicRequest, THierarchicResponse>(request);

            return requestHandler.ExecuteAsync(request);
        }
    }
}