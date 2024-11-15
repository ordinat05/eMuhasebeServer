using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.GetAllFileContentTableRows
{
    public sealed record GetAllFileContentTableRowsQuery() : IRequest<Result<List<FileContentTableRow>>>;
}