using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContent2s.GetAllFileContent2s;

public sealed record GetAllFileContent2sQuery() : IRequest<Result<List<FileContent2>>>;