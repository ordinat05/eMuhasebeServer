using eMuhasebeServer.Domain.Entities.Dtos;
using eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.KatCizelgeler.CreateKatCizelge
{
    public sealed record CizelgeOlusturCommand : IRequest<Result<NoDataDto>>
    {
        public KatCizelgeOlusturmaDto? KatCizelgeOlusturmaDto { get; set; }
    }
}
