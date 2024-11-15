using eMuhasebeServer.Domain.Entities.Dtos;
using MediatR;

namespace eMuhasebeServer.Application.Features.DocumentViewers.DeleteByIdDocumentViewerf1menu7;

public sealed record DeleteByIdDocumentViewerf1menu7Command(Guid Id) : IRequest<Response<bool>>;

