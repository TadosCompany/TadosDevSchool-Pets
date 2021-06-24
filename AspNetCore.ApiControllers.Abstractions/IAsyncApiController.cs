namespace AspnetCore.ApiControllers.Abstractions
{
    using Api.Requests.Abstractions;

    public interface IAsyncApiController
    {
        IAsyncRequestBuilder AsyncRequestBuilder { get; }
    }
}