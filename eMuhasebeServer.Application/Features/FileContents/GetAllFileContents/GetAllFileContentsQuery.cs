using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContents.GetAllFileContents;

public sealed record GetAllFileContentsQuery() : IRequest<Result<List<FileContent>>>;

