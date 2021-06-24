namespace Api.Requests.Abstractions
{
    public interface IRequest<out TResponse>
        where TResponse : IResponse
    {
    }
}