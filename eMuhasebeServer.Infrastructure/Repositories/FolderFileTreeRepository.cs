using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
namespace eMuhasebeServer.Infrastructure.Repositories;

internal sealed class FolderFileTreeRepository : Repository<FolderFileTree, ApplicationDbContext>, IFolderFileTreeRepository
{
    private readonly ApplicationDbContext _context;

    public FolderFileTreeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<FolderFileTree> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<FolderFileTree>()
            .Include(f => f.Children)
            .FirstAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<FolderFileTree>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<FolderFileTree>()
            .Include(f => f.Children)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(FolderFileTree folderFileTree, CancellationToken cancellationToken = default)
    {
        _context.Set<FolderFileTree>().Update(folderFileTree);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _context.Set<FolderFileTree>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
