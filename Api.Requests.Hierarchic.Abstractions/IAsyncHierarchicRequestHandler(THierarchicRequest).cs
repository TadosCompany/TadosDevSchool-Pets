namespace Api.Requests.Hierarchic.Abstractions
{
    public interface IAsyncHierarchicRequestHandler<in THierarchicRequest> : IAsyncWeaklyTypedHierarchicRequestHandler
        where THierarchicRequest : IHierarchicRequest
    {
    }
}