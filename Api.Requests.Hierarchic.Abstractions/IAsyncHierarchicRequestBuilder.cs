namespace Api.Requests.Hierarchic.Abstractions
{
    using System.Threading.Tasks;

    public interface IAsyncHierarchicRequestBuilder
    {
        Task ExecuteAsync<THierarchicRequest>(THierarchicRequest request)
            where THierarchicRequest : IHierarchicRequest;

        Task<THierarchicResponse> ExecuteAsync<THierarchicRequest, THierarchicResponse>(THierarchicRequest request)
            where THierarchicRequest : IHierarchicRequest<THierarchicResponse>
            where THierarchicResponse : IHierarchicResponse;
    }
}