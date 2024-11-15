using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Infrastructure.Repositories
{
    internal sealed class FileContentRepository : Repository<FileContent, ApplicationDbContext>, IFileContentRepository
    {
        private readonly ApplicationDbContext _context;

        public FileContentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<FileContent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Set<FileContent>()
                .FirstOrDefaultAsync(fc => fc.Id == id && !fc.IsDeleted, cancellationToken); 
        }


        public async Task<List<FileContent>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            //return await _context.Set<FileContent>()
            //    .Where(fc => !fc.IsDeleted)
            //    .ToListAsync(cancellationToken); 
            var fileContents = await _context.Set<FileContent>()
          .Where(fc => !fc.IsDeleted)
          .ToListAsync(cancellationToken);



            return fileContents;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var fileContent = await GetByIdAsync(id, cancellationToken);
            if (fileContent != null)
            {
                fileContent.IsDeleted = true; // Dosya içeriğini siler (soft delete)
                _context.Set<FileContent>().Update(fileContent);
            }
        }

        public async Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var fileContents = await GetAllAsync(cancellationToken);
            foreach (var fileContent in fileContents)
            {
                fileContent.IsDeleted = true; // Tüm dosya içeriklerini siler (soft delete)
                _context.Set<FileContent>().Update(fileContent);
            }
        }
    }
}
