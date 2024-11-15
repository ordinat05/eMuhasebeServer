using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Domain.MailKitMimeKit.Service
{
    internal sealed class KatCizelgeRepository : IKatCizelgeRepository
    {
        private readonly ApplicationDbContext _context;

        public KatCizelgeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(KatCizelge katCizelge, CancellationToken cancellationToken)
        {
            await _context.KatCizelgeler.AddAsync(katCizelge, cancellationToken);
        }

        public async Task<List<KatCizelge>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.KatCizelgeler.ToListAsync(cancellationToken);
        }

        public async Task<KatCizelge> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.KatCizelgeler.FindAsync(new object[] { id }, cancellationToken);
            return result ?? throw new KeyNotFoundException($"KatCizelgeHeader with id {id} not found.");
        }

        public async Task UpdateAsync(KatCizelge katCizelge, CancellationToken cancellationToken)
        {
            _context.KatCizelgeler.Update(katCizelge);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var katCizelge = await GetByIdAsync(id, cancellationToken);
            if (katCizelge != null)
            {
                _context.KatCizelgeler.Remove(katCizelge);
            }
        }
    }
}
