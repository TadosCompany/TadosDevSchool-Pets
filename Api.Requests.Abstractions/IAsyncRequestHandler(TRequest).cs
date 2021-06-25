namespace Api.Requests.Abstractions
{
    using System.Threading.Tasks;

    public interface IAsyncRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        Task ExecuteAsync(TRequest request);
    }
}