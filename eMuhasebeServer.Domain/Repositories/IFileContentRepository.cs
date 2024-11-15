using eMuhasebeServer.Domain.Entities;
using GenericRepository;

namespace eMuhasebeServer.Domain.Repositories
{
    public interface IFileContentRepository : IRepository<FileContent>
    {
        Task<FileContent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<FileContent>> GetAllAsync(CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default); 
        Task DeleteAllAsync(CancellationToken cancellationToken = default); 
    }
}
