namespace Api.Requests.Abstractions
{
    using System.Threading.Tasks;

    public interface IAsyncRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}