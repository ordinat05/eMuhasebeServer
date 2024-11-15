using eMuhasebeServer.Domain.Entities;
using GenericRepository;

namespace eMuhasebeServer.Domain.Repositories;


public interface ISideBarLeftRepository : IRepository<SideBarLeft>
{
    Task<SideBarLeft?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SideBarLeft>> GetAllAsync(CancellationToken cancellationToken = default);
    Task DeleteAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(SideBarLeft sideBarLeft, CancellationToken cancellationToken = default); 

}

