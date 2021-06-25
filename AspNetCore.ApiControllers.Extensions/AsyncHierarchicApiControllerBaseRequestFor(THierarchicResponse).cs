namespace AspnetCore.ApiControllers.Extensions
{
    using System.Threading.Tasks;
    using Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using Microsoft.AspNetCore.Mvc;

    public class AsyncHierarchicApiControllerBaseRequestFor<THierarchicResponse>
        where THierarchicResponse : IHierarchicResponse
    {
        private readonly ApiControllerBase _apiController;



        public AsyncHierarchicApiControllerBaseRequestFor(ApiControllerBase apiController)
        {
            _apiController = apiController;
        }



        public Task<IActionResult> With<THierarchicRequest>(THierarchicRequest request)
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            => _apiController.HierarchicRequestAsync<ApiControllerBase, THierarchicRequest, THierarchicResponse>(request);
    }
}