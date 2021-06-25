namespace AspnetCore.ApiControllers.Extensions
{
    using System.Threading.Tasks;
    using Abstractions;
    using Api.Requests.Abstractions;
    using Microsoft.AspNetCore.Mvc;

    public class AsyncApiControllerBaseRequestFor<TResponse>
        where TResponse : IResponse
    {
        private readonly ApiControllerBase _apiController;



        public AsyncApiControllerBaseRequestFor(ApiControllerBase apiController)
        {
            _apiController = apiController;
        }



        public Task<IActionResult> With<TRequest>(TRequest request)
            where TRequest : IRequest<TResponse>
            => _apiController.RequestAsync<ApiControllerBase, TRequest, TResponse>(request);
    }
}