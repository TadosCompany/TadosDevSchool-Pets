namespace AspnetCore.ApiControllers.Extensions
{
    using Abstractions;
    using Api.Requests.Hierarchic.Abstractions;

    public class AsyncHierarchicApiControllerBaseRequestBuilder
    {
        private readonly ApiControllerBase _apiController;



        public AsyncHierarchicApiControllerBaseRequestBuilder(ApiControllerBase apiController)
        {
            _apiController = apiController;
        }



        public AsyncHierarchicApiControllerBaseRequestFor<THierarchicResponse> For<THierarchicResponse>()
            where THierarchicResponse : IHierarchicResponse
            => new (_apiController);
    }
}