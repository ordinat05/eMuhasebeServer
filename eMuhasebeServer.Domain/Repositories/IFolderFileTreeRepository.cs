using eMuhasebeServer.Domain.Entities;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace eMuhasebeServer.Domain.Repositories;

public interface IFolderFileTreeRepository : IRepository<FolderFileTree>
{
    Task<FolderFileTree> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<FolderFileTree>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(FolderFileTree fileSystemNode, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
