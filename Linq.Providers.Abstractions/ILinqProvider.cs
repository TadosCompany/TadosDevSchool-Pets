namespace Linq.Providers.Abstractions
{
    using System.Linq;
    using Domain.Abstractions;


    public interface ILinqProvider
    {
        IQueryable<THasId> Query<THasId>() where THasId : class, IHasId, new();
    }
}