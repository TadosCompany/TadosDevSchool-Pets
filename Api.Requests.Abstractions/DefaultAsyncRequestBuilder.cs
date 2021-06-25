namespace Api.Requests.Abstractions
{
    using System;
    using System.Threading.Tasks;

    public class DefaultAsyncRequestBuilder : IAsyncRequestBuilder
    {
        private readonly IAsyncRequestHandlerFactory _asyncRequestHandlerFactory;



        public DefaultAsyncRequestBuilder(IAsyncRequestHandlerFactory asyncRequestHandlerFactory)
        {
            _asyncRequestHandlerFactory = asyncRequestHandlerFactory ?? throw new ArgumentNullException(nameof(asyncRequestHandlerFactory));
        }



        public Task ExecuteAsync<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            return _asyncRequestHandlerFactory.Create<TRequest>().ExecuteAsync(request);
        }

        public Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : IResponse
        {
            return _asyncRequestHandlerFactory.Create<TRequest, TResponse>().ExecuteAsync(request);
        }
    }
}