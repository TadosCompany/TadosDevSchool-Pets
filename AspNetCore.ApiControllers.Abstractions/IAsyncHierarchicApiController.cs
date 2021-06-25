namespace AspnetCore.ApiControllers.Abstractions
{
    using Api.Requests.Hierarchic.Abstractions;

    public interface IAsyncHierarchicApiController
    {
        IAsyncHierarchicRequestBuilder AsyncHierarchicRequestBuilder { get; }
    }
}