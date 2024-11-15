using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContents.DeleteFileContentById;

public sealed record DeleteFileContentByIdCommand(Guid Id) : IRequest<Result<string>>;

