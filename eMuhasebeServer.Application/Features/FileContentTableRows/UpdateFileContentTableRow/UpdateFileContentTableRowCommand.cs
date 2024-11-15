using MediatR;
using TS.Result;
using eMuhasebeServer.Domain.Entities;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.UpdateFileContentTableRow
{
    public sealed record UpdateFileContentTableRowCommand(
        Guid Id,
        bool IsActive,
        string Konu,
        string Not,
        string ToggleButtonState,
        DateTime? SaveDateTime,
        string FileCount,
        string GoToLink,
        int Order,
        string Status,  
        string Color    
    ) : IRequest<Result<FileContentTableRow>>;
}