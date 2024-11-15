using eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.KatCizelgeler.KatCizelgeHeaderSearchPatterns;


public class KatCizelgeHeaderSearchPatternQuery : IRequest<Result<List<KatCizelgeListDto>>>
{
    public string? Filtre { get; set; }
}
