namespace AspnetCore.ApiControllers.Abstractions
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public interface IHasDefaultSuccessActionResult
    {
        Func<IActionResult> Success { get; }
    }
}