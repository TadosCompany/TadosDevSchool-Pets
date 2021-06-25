namespace AspnetCore.ApiControllers.Abstractions
{
    using System;
    using Api.Requests.Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Persistence.Transactions.Behaviors;

    [ApiController]
    public class ApiControllerBase
        : ControllerBase,
            IAsyncApiController,
            IAsyncHierarchicApiController,
            IHasDefaultSuccessActionResult,
            IHasDefaultResponseSuccessActionResult,
            IHasDefaultHierarchicResponseSuccessActionResult,
            IHasDefaultFailActionResult,
            IHasInvalidModelStateActionResult,
            IShouldPerformCommit
    {
        private readonly IAsyncRequestBuilder _asyncRequestBuilder;
        private readonly IAsyncHierarchicRequestBuilder _asyncHierarchicRequestBuilder;
        private readonly IExpectCommit _commitPerformer;



        public ApiControllerBase(
            IAsyncRequestBuilder asyncRequestBuilder,
            IAsyncHierarchicRequestBuilder asyncHierarchicRequestBuilder,
            IExpectCommit commitPerformer)
        {
            _asyncRequestBuilder = asyncRequestBuilder ?? throw new ArgumentNullException(nameof(asyncRequestBuilder));
            _asyncHierarchicRequestBuilder = asyncHierarchicRequestBuilder ?? throw new ArgumentNullException(nameof(asyncHierarchicRequestBuilder));
            _commitPerformer = commitPerformer ?? throw new ArgumentNullException(nameof(commitPerformer));
        }



        public virtual Func<IActionResult> Success
            => () => new OkResult();

        public virtual Func<TResponse, IActionResult> ResponseSuccess<TResponse>()
            where TResponse : IResponse
            => (TResponse response) => new OkObjectResult(response);

        public virtual Func<THierarchicResponse, IActionResult> HierarchicResponseSuccess<THierarchicResponse>()
            where THierarchicResponse : IHierarchicResponse
            => (THierarchicResponse response) => new OkObjectResult(response);

        public virtual Func<Exception, IActionResult> Fail
            => (Exception exception) => new BadRequestObjectResult(exception.Message);

        public virtual Func<ModelStateDictionary, IActionResult> InvalidModelState
            => (ModelStateDictionary modelState) => new BadRequestObjectResult(new ValidationProblemDetails(modelState).Errors);



        IAsyncRequestBuilder IAsyncApiController.AsyncRequestBuilder => _asyncRequestBuilder;

        IAsyncHierarchicRequestBuilder IAsyncHierarchicApiController.AsyncHierarchicRequestBuilder => _asyncHierarchicRequestBuilder;

        IExpectCommit IShouldPerformCommit.CommitPerformer => _commitPerformer;
    }
}
