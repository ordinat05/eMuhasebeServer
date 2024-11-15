using eMuhasebeServer.Domain.Entities;
using GenericRepository;

namespace eMuhasebeServer.Domain.Repositories;

public interface IDocumentViewerRepository : IRepository<DocumentViewerf1menu7>
{
    Task<DocumentViewerf1menu7?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<DocumentViewerf1menu7>> GetAllAsync(CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(DocumentViewerf1menu7 entity, CancellationToken cancellationToken = default);
}
