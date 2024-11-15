using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Entities.Dtos;
using MediatR;

namespace eMuhasebeServer.Application.Features.DocumentViewers.UpdateSortIndexf1menu7;

public sealed record SortIndexUpdateItem(Guid Id, int SortIndex);

public sealed record UpdateSortIndexf1menu7Command(List<SortIndexUpdateItem> SortIndexs)
    : IRequest<Response<List<DocumentViewerf1menu7>>>;