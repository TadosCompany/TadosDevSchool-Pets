namespace AspnetCore.ApiControllers.Extensions
{
    using Abstractions;
    using Api.Requests.Abstractions;

    public class AsyncApiControllerBaseRequestBuilder
    {
        private readonly ApiControllerBase _apiController;



        public AsyncApiControllerBaseRequestBuilder(ApiControllerBase apiController)
        {
            _apiController = apiController;
        }



        public AsyncApiControllerBaseRequestFor<TResponse> For<TResponse>() 
            where TResponse : IResponse 
            => new (_apiController);
    }
}