using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Infrastructure.Repositories;

internal sealed class DocumentViewerRepository : Repository<DocumentViewerf1menu7, ApplicationDbContext>, IDocumentViewerRepository
{
    private readonly ApplicationDbContext _context;

    public DocumentViewerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DocumentViewerf1menu7?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<DocumentViewerf1menu7>()
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public async Task<List<DocumentViewerf1menu7>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<DocumentViewerf1menu7>()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            entity.IsDeleted = true;
            _context.Set<DocumentViewerf1menu7>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateAsync(DocumentViewerf1menu7 entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var existingEntity = await GetByIdAsync(entity.Id, cancellationToken);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}