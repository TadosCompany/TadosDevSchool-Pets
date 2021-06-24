namespace AspnetCore.ApiControllers.Abstractions
{
    using System;
    using Api.Requests.Abstractions;
    using Microsoft.AspNetCore.Mvc;

    public interface IHasDefaultResponseSuccessActionResult
    {
        Func<TResponse, IActionResult> ResponseSuccess<TResponse>()
            where TResponse : IResponse;
    }
}