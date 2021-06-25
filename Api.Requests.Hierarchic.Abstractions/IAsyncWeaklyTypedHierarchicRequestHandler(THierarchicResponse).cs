namespace Api.Requests.Hierarchic.Abstractions
{
    using System.Threading.Tasks;

    public interface IAsyncWeaklyTypedHierarchicRequestHandler<THierarchicResponse>
        where THierarchicResponse : IHierarchicResponse
    {
        Task<THierarchicResponse> ExecuteAsync(IHierarchicRequest<THierarchicResponse> request);
    }
}