using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Infrastructure.Repositories
{
    internal sealed class FileContentTableRowRepository : Repository<FileContentTableRow, ApplicationDbContext>, IFileContentTableRowRepository
    {
        private readonly ApplicationDbContext _context;

        public FileContentTableRowRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FileContentTableRow?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<FileContentTableRow>()
                .Include(fctr => fctr.FileContent2ler)
                .FirstOrDefaultAsync(fctr => fctr.Id == id && !fctr.IsDeleted, cancellationToken);
        }

        public async Task<List<FileContentTableRow>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<FileContentTableRow>()
                //.Include(fctr => fctr.FileContent2ler)
                .Where(fctr => !fctr.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var fileContentTableRow = await GetByIdAsync(id, cancellationToken);
            if (fileContentTableRow != null)
            {
                fileContentTableRow.IsDeleted = true;
                if (fileContentTableRow.FileContent2ler != null)
                {
                    foreach (var fileContent2 in fileContentTableRow.FileContent2ler)
                    {
                        fileContent2.IsDeleted = true;
                    }
                }
                _context.Set<FileContentTableRow>().Update(fileContentTableRow);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(FileContentTableRow fileContentTableRow, CancellationToken cancellationToken = default)
        {
            _context.Set<FileContentTableRow>().Update(fileContentTableRow);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}