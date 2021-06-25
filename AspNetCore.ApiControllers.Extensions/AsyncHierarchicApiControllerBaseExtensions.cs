namespace AspnetCore.ApiControllers.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using Microsoft.AspNetCore.Mvc;

    public static class AsyncHierarchicApiControllerBaseExtensions
    {
        public static Task<IActionResult> HierarchicRequestAsync<THierarchicRequest, THierarchicResponse>(
            this ApiControllerBase apiController,
            THierarchicRequest hierarchicRequest)
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            where THierarchicResponse : IHierarchicResponse
            => apiController.HierarchicRequestAsync<ApiControllerBase, THierarchicRequest, THierarchicResponse>(hierarchicRequest);

        public static Task<IActionResult> HierarchicRequestAsync<THierarchicRequest, THierarchicResponse>(
            this ApiControllerBase apiController,
            THierarchicRequest hierarchicRequest,
            Func<THierarchicResponse, IActionResult> success)
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            where THierarchicResponse : IHierarchicResponse
            => apiController.HierarchicRequestAsync<ApiControllerBase, THierarchicRequest, THierarchicResponse>(hierarchicRequest, success);



        public static AsyncHierarchicApiControllerBaseRequestBuilder HierarchicRequestAsync(
            this ApiControllerBase apiController) 
            => new (apiController);
    }
}
