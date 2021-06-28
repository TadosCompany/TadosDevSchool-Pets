namespace Repositories.Abstractions
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Abstractions;


    public interface IAsyncRepository<THasId> where THasId : IHasId
    {
        Task AddAsync(THasId objectWithId, CancellationToken cancellationToken = default);

        Task DeleteAsync(THasId objectWithId, CancellationToken cancellationToken = default);

        Task UpdateAsync(THasId objectWithId, CancellationToken cancellationToken = default);

        Task<THasId> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<List<THasId>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}