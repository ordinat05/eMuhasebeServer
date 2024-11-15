using AutoMapper;
using eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.KatCizelgeler.GetAllKatCizelge
{
    public sealed class GetAllKatCizelgeQueryHandler : IRequestHandler<GetAllKatCizelgeQuery, Result<List<KatCizelgeListDto>>>
    {
        private readonly IKatCizelgeHeaderRepository _headerRepository;
        private readonly IMapper _mapper;

        public GetAllKatCizelgeQueryHandler(IKatCizelgeHeaderRepository headerRepository, IMapper mapper)
        {
            _headerRepository = headerRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<KatCizelgeListDto>>> Handle(GetAllKatCizelgeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = await _headerRepository.GetAll()
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
}
