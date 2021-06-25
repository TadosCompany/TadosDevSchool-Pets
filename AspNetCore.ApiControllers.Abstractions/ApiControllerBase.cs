namespace AspnetCore.ApiControllers.Abstractions
{
    using System;
    using Api.Requests.Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    [ApiController]
    public class ApiControllerBase
        : ControllerBase,
            IAsyncApiController,
            IAsyncHierarchicApiController,
            IHasDefaultSuccessActionResult,
            IHasDefaultResponseSuccessActionResult,
            IHasDefaultHierarchicResponseSuccessActionResult,
            IHasDefaultFailActionResult,
            IHasInvalidModelStateActionResult
    {
        private readonly IAsyncRequestBuilder _asyncRequestBuilder;
        private readonly IAsyncHierarchicRequestBuilder _asyncHierarchicRequestBuilder;



        public ApiControllerBase(
            IAsyncRequestBuilder asyncRequestBuilder,
            IAsyncHierarchicRequestBuilder asyncHierarchicRequestBuilder)
        {
            _asyncRequestBuilder = asyncRequestBuilder ?? throw new ArgumentNullException(nameof(asyncRequestBuilder));
            _asyncHierarchicRequestBuilder = asyncHierarchicRequestBuilder ?? throw new ArgumentNullException(nameof(asyncHierarchicRequestBuilder));
        }



        public Func<IActionResult> Success
            => () => new OkResult();

        public Func<TResponse, IActionResult> ResponseSuccess<TResponse>()
            where TResponse : IResponse
            => (response) => new OkObjectResult(response);

        public Func<THierarchicResponse, IActionResult> HierarchicResponseSuccess<THierarchicResponse>()
            where THierarchicResponse : IHierarchicResponse
            => (response) => new OkObjectResult(response);

        public Func<Exception, IActionResult> Fail
            => (exception) => new BadRequestObjectResult(exception.Message);

        public Func<ModelStateDictionary, IActionResult> InvalidModelState
            => (modelState) => new BadRequestObjectResult(new ValidationProblemDetails(modelState).Errors);

        IAsyncRequestBuilder IAsyncApiController.AsyncRequestBuilder => _asyncRequestBuilder;

        IAsyncHierarchicRequestBuilder IAsyncHierarchicApiController.AsyncHierarchicRequestBuilder => _asyncHierarchicRequestBuilder;
    }
}
