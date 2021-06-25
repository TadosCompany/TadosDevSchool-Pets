namespace Api.Requests.Abstractions
{
    using System.Threading.Tasks;

    public interface IAsyncRequestBuilder
    {
        Task ExecuteAsync<TRequest>(TRequest request)
            where TRequest : IRequest;

        Task<TRequestResult> ExecuteAsync<TRequest, TRequestResult>(TRequest request)
            where TRequest : IRequest<TRequestResult>
            where TRequestResult : IResponse;
    }
}