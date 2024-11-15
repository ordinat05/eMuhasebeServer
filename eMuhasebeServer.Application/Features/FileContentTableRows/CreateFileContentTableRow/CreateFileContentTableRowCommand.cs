using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.CreateFileContentTableRow
{
    public sealed record CreateFileContentTableRowCommand(
        string Konu,
        string Not,
        string ToggleButtonState,
        DateTime? SaveDateTime,
        string GoToLink,
        string FileCount,
        bool IsActive,
        int Order,
        bool IsDeleted,
        string Status,  
        string Color    
    ) : IRequest<Result<FileContentTableRow>>;
}