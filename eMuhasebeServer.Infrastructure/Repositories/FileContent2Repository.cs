using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Infrastructure.Repositories
{
    internal sealed class FileContent2Repository : Repository<FileContent2, ApplicationDbContext>, IFileContent2Repository
    {
        private readonly ApplicationDbContext _context;

        public FileContent2Repository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<FileContent2?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Set<FileContent2>()
                .FirstOrDefaultAsync(fc => fc.Id == id && !fc.IsDeleted, cancellationToken);
        }

        public async Task<List<FileContent2>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<FileContent2>()
                .Where(fc => !fc.IsDeleted)
                .OrderBy(fc => fc.SortIndex)
                .ToListAsync(cancellationToken);
        }
        public async Task<List<FileContent2>> GetAllByFileContentTableRowIdAsync(Guid fileContentTableRowId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<FileContent2>()
                .Where(fc => fc.FileContentTableRowId == fileContentTableRowId)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var fileContent = await GetByIdAsync(id, cancellationToken);
            if (fileContent != null)
            {
                fileContent.IsDeleted = true; // Soft delete
                _context.Set<FileContent2>().Update(fileContent);
            }
        }

        public async Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var fileContents = await GetAllAsync(cancellationToken);
            foreach (var fileContent in fileContents)
            {
                fileContent.IsDeleted = true; // Soft delete
                _context.Set<FileContent2>().Update(fileContent);
            }
        }
    }
}