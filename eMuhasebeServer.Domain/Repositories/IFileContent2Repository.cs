using eMuhasebeServer.Domain.Entities;
using GenericRepository;

namespace eMuhasebeServer.Domain.Repositories
{
    public interface IFileContent2Repository : IRepository<FileContent2>
    {
        Task<FileContent2?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<FileContent2>> GetAllAsync(CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAllAsync(CancellationToken cancellationToken = default);
        Task<List<FileContent2>> GetAllByFileContentTableRowIdAsync(Guid fileContentTableRowId, CancellationToken cancellationToken = default);


    }
}