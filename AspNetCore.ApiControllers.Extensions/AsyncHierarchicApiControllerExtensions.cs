namespace AspnetCore.ApiControllers.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using Microsoft.AspNetCore.Mvc;

    public static class AsyncHierarchicApiControllerExtensions
    {
        public static Task<IActionResult> HierarchicRequestAsync<TApiController, THierarchicRequest>(
            this TApiController apiController,
            THierarchicRequest hierarchicRequest)
            where TApiController : 
                ControllerBase, 
                IAsyncHierarchicApiController, 
                IHasDefaultSuccessActionResult, 
                IHasDefaultFailActionResult, 
                IHasInvalidModelStateActionResult,
                IShouldPerformCommit
            where THierarchicRequest : IHierarchicRequest
            => HierarchicRequestAsync(
                apiController,
                hierarchicRequest,
                apiController.Success,
                apiController.Fail);

        public static Task<IActionResult> HierarchicRequestAsync<TApiController, THierarchicRequest>(
            this TApiController apiController,
            THierarchicRequest hierarchicRequest,
            Func<IActionResult> success)
            where TApiController : 
                ControllerBase, 
                IAsyncHierarchicApiController, 
                IHasDefaultFailActionResult, 
                IHasInvalidModelStateActionResult,
                IShouldPerformCommit
            where THierarchicRequest : IHierarchicRequest
            => HierarchicRequestAsync(
                apiController,
                hierarchicRequest,
                success,
                apiController.Fail);

        public static async Task<IActionResult> HierarchicRequestAsync<TApiController, THierarchicRequest>(
            this TApiController apiController,
            THierarchicRequest hierarchicRequest,
            Func<IActionResult> success,
            Func<Exception, IActionResult> fail)
            where TApiController : 
                ControllerBase, 
                IAsyncHierarchicApiController, 
                IHasInvalidModelStateActionResult,
                IShouldPerformCommit
            where THierarchicRequest : IHierarchicRequest
        {
            if (apiController == null)
                throw new ArgumentNullException(nameof(apiController));

            if (hierarchicRequest == null)
                throw new ArgumentNullException(nameof(hierarchicRequest));

            if (!apiController.ModelState.IsValid)
                return apiController.InvalidModelState(apiController.ModelState);

            try
            {
                await apiController.AsyncHierarchicRequestBuilder.ExecuteAsync(hierarchicRequest);

                apiController.CommitPerformer.PerformCommit();

                return success();
            }
            catch (Exception exception)
            {
                return fail(exception);
            }
        }



        public static Task<IActionResult> HierarchicRequestAsync<TApiController, THierarchicRequest, THierarchicResponse>(
            this TApiController apiController,
            THierarchicRequest hierarchicRequest)
            where TApiController : 
                ControllerBase, 
                IAsyncHierarchicApiController, 
                IHasDefaultHierarchicResponseSuccessActionResult, 
                IHasDefaultFailActionResult, 
                IHasInvalidModelStateActionResult,
                IShouldPerformCommit
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            where THierarchicResponse : IHierarchicResponse
            => HierarchicRequestAsync(
                apiController,
                hierarchicRequest,
                apiController.HierarchicResponseSuccess<THierarchicResponse>(),
                apiController.Fail);

        public static Task<IActionResult> HierarchicRequestAsync<TApiController, THierarchicRequest, THierarchicResponse>(
            this TApiController apiController,
            THierarchicRequest hierarchicRequest,
            Func<THierarchicResponse, IActionResult> success)
            where TApiController : 
                ControllerBase, 
                IAsyncHierarchicApiController, 
                IHasDefaultFailActionResult, 
                IHasInvalidModelStateActionResult,
                IShouldPerformCommit
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            where THierarchicResponse : IHierarchicResponse
            => HierarchicRequestAsync(
                apiController,
                hierarchicRequest,
                success,
                apiController.Fail);

        public static async Task<IActionResult> HierarchicRequestAsync<TApiController, THierarchicRequest, THierarchicResponse>(
            this TApiController apiController,
            THierarchicRequest hierarchicRequest,
            Func<THierarchicResponse, IActionResult> success,
            Func<Exception, IActionResult> fail)
            where TApiController : 
                ControllerBase, 
                IAsyncHierarchicApiController, 
                IHasInvalidModelStateActionResult,
                IShouldPerformCommit
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            where THierarchicResponse : IHierarchicResponse
        {
            if (apiController == null)
                throw new ArgumentNullException(nameof(apiController));

            if (hierarchicRequest == null)
                throw new ArgumentNullException(nameof(hierarchicRequest));

            if (!apiController.ModelState.IsValid)
                return apiController.InvalidModelState(apiController.ModelState);

            try
            {
                THierarchicResponse hierarchicResponse = await apiController.AsyncHierarchicRequestBuilder.ExecuteAsync<THierarchicRequest, THierarchicResponse>(hierarchicRequest);

                apiController.CommitPerformer.PerformCommit();

                return success(hierarchicResponse);
            }
            catch (Exception exception)
            {
                return fail(exception);
            }
        }
    }
}
