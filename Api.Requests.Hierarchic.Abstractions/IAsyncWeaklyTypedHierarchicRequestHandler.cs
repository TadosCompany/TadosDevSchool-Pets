namespace Api.Requests.Hierarchic.Abstractions
{
    using System.Threading.Tasks;

    public interface IAsyncWeaklyTypedHierarchicRequestHandler
    {
        Task ExecuteAsync(IHierarchicRequest request);
    }
}