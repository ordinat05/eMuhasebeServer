using eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.KatCizelgeler.GetAllKatCizelge
{
    public sealed record GetAllKatCizelgeQuery() : IRequest<Result<List<KatCizelgeListDto>>>;

}
