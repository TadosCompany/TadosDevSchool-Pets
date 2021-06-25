namespace Api.Requests.Hierarchic.Abstractions
{
    public interface IAsyncHierarchicRequestHandler<in THierarchicRequest, THierarchicResponse> : IAsyncWeaklyTypedHierarchicRequestHandler<THierarchicResponse>
        where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
        where THierarchicResponse : IHierarchicResponse
    {
    }
}