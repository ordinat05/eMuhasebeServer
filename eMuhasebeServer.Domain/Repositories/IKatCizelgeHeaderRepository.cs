using eMuhasebeServer.Domain.Entities;
using System.Linq.Expressions;

namespace eMuhasebeServer.Domain.Repositories
{
    public interface IKatCizelgeHeaderRepository
    {
        Task AddAsync(KatCizelgeHeader katCizelgeHeader, CancellationToken cancellationToken);
        Task<List<KatCizelgeHeader>> GetAllAsync(CancellationToken cancellationToken);
        Task<KatCizelgeHeader> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(KatCizelgeHeader katCizelgeHeader, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        IQueryable<KatCizelgeHeader> GetAll();
        Task<List<KatCizelgeHeader>> SearchAsync(Expression<Func<KatCizelgeHeader, bool>> predicate, CancellationToken cancellationToken);

    }
}
