using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContent2s.GetAllByFileContentTableRowId;

public sealed record GetAllByFileContentTableRowIdCommand(Guid fileContentTableRowId) : IRequest<Result<List<FileContent2>>>;
