using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContent2s.DeleteFileContent2sById;

public sealed record DeleteFileContent2ByIdCommand(Guid Id) : IRequest<Result<string>>;