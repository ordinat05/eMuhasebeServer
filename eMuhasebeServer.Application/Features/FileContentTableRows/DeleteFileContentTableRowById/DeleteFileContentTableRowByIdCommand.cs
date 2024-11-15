using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.DeleteFileContentTableRowById
{
    public sealed record DeleteFileContentTableRowByIdCommand(Guid Id) : IRequest<Result<Unit>>;
}