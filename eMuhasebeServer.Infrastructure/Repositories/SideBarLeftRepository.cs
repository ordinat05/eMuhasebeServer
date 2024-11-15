using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Infrastructure.Repositories;

internal sealed class SideBarLeftRepository : Repository<SideBarLeft, ApplicationDbContext>, ISideBarLeftRepository
{
    private readonly ApplicationDbContext _context;

    public SideBarLeftRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<SideBarLeft?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Set<SideBarLeft>()
        .FirstOrDefaultAsync(sl => sl.Id == id, cancellationToken);
    }

    public Task<IEnumerable<SideBarLeft>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Set<SideBarLeft>().AsEnumerable());
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _context.Set<SideBarLeft>().Remove(entity);
        }
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken = default)
    {
        var allItems = await _context.Set<SideBarLeft>().ToListAsync(cancellationToken);
        _context.Set<SideBarLeft>().RemoveRange(allItems);
    }
    public async Task UpdateAsync(SideBarLeft sideBarLeft, CancellationToken cancellationToken = default)
    {
        _context.Set<SideBarLeft>().Update(sideBarLeft);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
