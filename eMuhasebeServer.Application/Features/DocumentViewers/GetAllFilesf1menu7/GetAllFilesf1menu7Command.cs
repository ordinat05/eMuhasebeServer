using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DocumentViewers.GetAllFilesf1menu7
{
    public sealed record GetAllFilesf1menu7Command() : IRequest<Result<List<DocumentViewerf1menu7>>>;
}