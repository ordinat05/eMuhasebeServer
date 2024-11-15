using eMuhasebeServer.Domain.Entities;

namespace eMuhasebeServer.Application.InterfaceService;

public interface IFindIdinTheTableService
{
    Task<object?> GetEntityByTableDocumentViewersf1menu7(string tableName, Guid id, CancellationToken cancellationToken);
    // Burada 1. Amaç Gösterilecek olan Dosyanın adını GuidId yeniden oluşturmak.


}
