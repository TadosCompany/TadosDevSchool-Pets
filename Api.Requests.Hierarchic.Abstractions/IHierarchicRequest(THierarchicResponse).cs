namespace Api.Requests.Hierarchic.Abstractions
{
    public interface IHierarchicRequest<out THierarchicResponse>
        where THierarchicResponse : IHierarchicResponse
    {
    }
}