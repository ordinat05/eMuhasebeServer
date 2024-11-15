using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eMuhasebeServer.Infrastructure.Repositories
{
    internal sealed class KatCizelgeHeaderRepository : IKatCizelgeHeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public KatCizelgeHeaderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(KatCizelgeHeader katCizelgeHeader, CancellationToken cancellationToken)
        {
            await _context.KatCizelgeHeaders.AddAsync(katCizelgeHeader, cancellationToken);
        }

        public async Task<List<KatCizelgeHeader>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.KatCizelgeHeaders.ToListAsync(cancellationToken);
        }

        public async Task<KatCizelgeHeader> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.KatCizelgeHeaders.FindAsync(new object[] { id }, cancellationToken);
            return result ?? throw new KeyNotFoundException($"KatCizelgeHeader with id {id} not found.");
        }

        public async Task UpdateAsync(KatCizelgeHeader katCizelgeHeader, CancellationToken cancellationToken)
        {
            _context.KatCizelgeHeaders.Update(katCizelgeHeader);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var katCizelgeHeader = await GetByIdAsync(id, cancellationToken);
            if (katCizelgeHeader != null)
            {
                _context.KatCizelgeHeaders.Remove(katCizelgeHeader);
            }
        }

        public IQueryable<KatCizelgeHeader> GetAll()
        {
            return _context.KatCizelgeHeaders.AsQueryable();
        }
        public async Task<List<KatCizelgeHeader>> SearchAsync(Expression<Func<KatCizelgeHeader, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.KatCizelgeHeaders
                .Where(predicate)
                .Include(h => h.KatCizelgeler)
                .ToListAsync(cancellationToken);
        }
    }
}
