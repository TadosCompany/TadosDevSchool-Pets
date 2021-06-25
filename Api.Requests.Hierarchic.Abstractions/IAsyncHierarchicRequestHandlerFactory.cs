namespace Api.Requests.Hierarchic.Abstractions
{
    public interface IAsyncHierarchicRequestHandlerFactory
    {
        IAsyncHierarchicRequestHandler<THierarchicRequest> Create<THierarchicRequest>(THierarchicRequest request)
            where THierarchicRequest : IHierarchicRequest;

        IAsyncHierarchicRequestHandler<THierarchicRequest, THierarchicResponse> Create<THierarchicRequest, THierarchicResponse>(THierarchicRequest request)
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            where THierarchicResponse : IHierarchicResponse;
    }
}