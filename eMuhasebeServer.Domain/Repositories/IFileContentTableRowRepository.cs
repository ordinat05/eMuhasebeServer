using eMuhasebeServer.Domain.Entities;
using GenericRepository;

namespace eMuhasebeServer.Domain.Repositories
{
    public interface IFileContentTableRowRepository : IRepository<FileContentTableRow>
    {
        Task<FileContentTableRow?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<FileContentTableRow>> GetAllAsync(CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(FileContentTableRow fileContentTableRow, CancellationToken cancellationToken = default);
    }
}