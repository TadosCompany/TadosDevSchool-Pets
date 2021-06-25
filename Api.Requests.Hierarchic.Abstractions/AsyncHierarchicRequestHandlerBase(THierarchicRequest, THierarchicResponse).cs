namespace Api.Requests.Hierarchic.Abstractions
{
    using System.Threading.Tasks;

    public abstract class AsyncHierarchicRequestHandlerBase<THierarchicRequest, THierarchicResponse> : IAsyncHierarchicRequestHandler<THierarchicRequest, THierarchicResponse>
        where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
        where THierarchicResponse : IHierarchicResponse
    {
        protected abstract Task<THierarchicResponse> ExecuteAsync(THierarchicRequest request);



        Task<THierarchicResponse> IAsyncWeaklyTypedHierarchicRequestHandler<THierarchicResponse>.ExecuteAsync(IHierarchicRequest<THierarchicResponse> request)
        {
            return ExecuteAsync((THierarchicRequest)request);
        }
    }
}