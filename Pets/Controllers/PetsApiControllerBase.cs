namespace Pets.Controllers
{
    using System;
    using Api.Requests.Abstractions;
    using Api.Requests.Hierarchic.Abstractions;
    using AspnetCore.ApiControllers.Abstractions;
    using global::Domain.Abstractions;
    using global::Persistence.Transactions.Behaviors;
    using Microsoft.AspNetCore.Mvc;

    public class PetsApiControllerBase : ApiControllerBase
    {
        public PetsApiControllerBase(
            IAsyncRequestBuilder asyncRequestBuilder, 
            IAsyncHierarchicRequestBuilder asyncHierarchicRequestBuilder, 
            IExpectCommit commitPerformer) 
            : base(
                asyncRequestBuilder, 
                asyncHierarchicRequestBuilder, 
                commitPerformer)
        {
        }



        public override Func<Exception, IActionResult> Fail => ProcessFail;



        private static IActionResult ProcessFail(Exception exception)
        {
            if (exception is IDomainException)
                return new BadRequestObjectResult(new
                {
                    Type = exception.GetType().Name,
                    Message = exception.Message
                });

            throw exception;
        }
    }
}
