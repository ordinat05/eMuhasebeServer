using AutoMapper;
using eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;
using eMuhasebeServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.KatCizelgeler.KatCizelgeHeaderSearchPatterns;

public class KatCizelgeHeaderSearchPatternQueryHandler : IRequestHandler<KatCizelgeHeaderSearchPatternQuery, Result<List<KatCizelgeListDto>>>
{
    private readonly IKatCizelgeHeaderRepository _headerRepository;
    private readonly IMapper _mapper;

    public KatCizelgeHeaderSearchPatternQueryHandler(IKatCizelgeHeaderRepository headerRepository, IMapper mapper)
    {
        _headerRepository = headerRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<KatCizelgeListDto>>> Handle(KatCizelgeHeaderSearchPatternQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _headerRepository.GetAll();

            if (!string.IsNullOrEmpty(request.Filtre))
            {
                query = query.Where(h => h.Tag != null && h.Tag.Contains(request.Filtre));
            }

            var headers = await query
                .Include(h => h.KatCizelgeler)
                .ToListAsync(cancellationToken);

            var dtoList = headers.Select(header => new KatCizelgeListDto
            {
                CizelgeId = header.Id,
                HaveTblIlce = header.HaveTblIlceId,
                Data = _mapper.Map<List<KatCizelgeDto>>(header.KatCizelgeler)
            }).ToList();

            return Result<List<KatCizelgeListDto>>.Succeed(dtoList);
        }
        catch (Exception ex)
        {
            return Result<List<KatCizelgeListDto>>.Failure(500, $"Hata: {ex.Message}");
        }
    }
}