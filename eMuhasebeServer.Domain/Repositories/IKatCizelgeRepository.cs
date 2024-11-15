using eMuhasebeServer.Domain.Entities;

namespace eMuhasebeServer.Domain.Repositories
{
    public interface IKatCizelgeRepository
    {
        Task AddAsync(KatCizelge katCizelge, CancellationToken cancellationToken);
        Task<List<KatCizelge>> GetAllAsync(CancellationToken cancellationToken);
        Task<KatCizelge> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(KatCizelge katCizelge, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
