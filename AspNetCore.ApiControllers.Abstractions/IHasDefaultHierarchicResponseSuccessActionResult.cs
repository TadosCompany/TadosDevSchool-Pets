namespace AspnetCore.ApiControllers.Abstractions
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Api.Requests.Hierarchic.Abstractions;

    public interface IHasDefaultHierarchicResponseSuccessActionResult
    {
        Func<THierarchicResponse, IActionResult> HierarchicResponseSuccess<THierarchicResponse>()
            where THierarchicResponse : IHierarchicResponse;
    }
}