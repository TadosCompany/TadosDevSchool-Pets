namespace AspnetCore.ApiControllers.Abstractions
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public interface IHasDefaultFailActionResult
    {
        Func<Exception, IActionResult> Fail { get; }
    }
}