using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.UpdateSortIndex
{
    public sealed record UpdateSortIndexRequest(Guid Id, int Order);

    public sealed record UpdateSortIndexCommand(List<UpdateSortIndexRequest> Orders)
        : IRequest<Result<List<FileContentTableRow>>>;
}
