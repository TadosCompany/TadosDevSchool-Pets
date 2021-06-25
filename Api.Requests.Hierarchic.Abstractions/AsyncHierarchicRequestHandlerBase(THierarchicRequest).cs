namespace Api.Requests.Hierarchic.Abstractions
{
    using System.Threading.Tasks;

    public abstract class AsyncHierarchicRequestHandlerBase<THierarchicRequest> : IAsyncHierarchicRequestHandler<THierarchicRequest>
        where THierarchicRequest : IHierarchicRequest
    {
        protected abstract Task ExecuteAsync(THierarchicRequest request);



        Task IAsyncWeaklyTypedHierarchicRequestHandler.ExecuteAsync(IHierarchicRequest request)
        {
            return ExecuteAsync((THierarchicRequest)request);
        }
    }
}