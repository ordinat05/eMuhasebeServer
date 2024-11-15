using AutoMapper;
using eMuhasebeServer.Domain.Entities.Dtos;
using eMuhasebeServer.Domain.Entities;
using GenericRepository;
using MediatR;
using Newtonsoft.Json;
using TS.Result;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Application.InterfaceService;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Application.Features.KatCizelgeler.CreateKatCizelge
{
    public sealed record CizelgeOlusturCommandHandler : IRequestHandler<CizelgeOlusturCommand, Result<NoDataDto>>
    {
        private readonly IKatCizelgeHeaderRepository _headerRepository;
        private readonly IKatCizelgeRepository _cizelgeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CizelgeOlusturCommandHandler(
            IKatCizelgeHeaderRepository headerRepository,
            IKatCizelgeRepository cizelgeRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileService fileService)
        {
            _headerRepository = headerRepository;
            _cizelgeRepository = cizelgeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Result<NoDataDto>> Handle(CizelgeOlusturCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.KatCizelgeOlusturmaDto;

                if (dto == null)
                {
                    return Result<NoDataDto>.Failure(400, "KatCizelgeOlusturmaDto is null");
                }
                // LastNumber işlemi
                int lastNumber = await GetLastNumber(cancellationToken);

                var header = _mapper.Map<KatCizelgeHeader>(dto);
                header.No = lastNumber;

                // Dosya yükleme işlemi
                if (dto.File != null)
                {
                    string filePath = await _fileService.UploadFileSrvAsync(dto.File, "KatCizelge", cancellationToken);
                    header.FilePath = filePath;
                }

                await _headerRepository.AddAsync(header, cancellationToken);

                if (!string.IsNullOrEmpty(dto?.CizelgeList))
                {
                    var cizelgeItems = JsonConvert.DeserializeObject<List<KatCizelge>>(dto.CizelgeList);
                    foreach (var item in cizelgeItems)
                    {
                        item.KatCizelgeHeaderId = header.Id;
                        await _cizelgeRepository.AddAsync(item, cancellationToken);
                    }
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<NoDataDto>.Succeed(new NoDataDto());
            }
            catch (Exception ex)
            {
                return Result<NoDataDto>.Failure(500, $"Hata: {ex.Message}");
            }
        }

        private async Task<int> GetLastNumber(CancellationToken cancellationToken)
        {
            var lastHeader = await _headerRepository.GetAll()
                .OrderByDescending(h => h.No)
                .FirstOrDefaultAsync(cancellationToken);

            return (lastHeader?.No ?? 0) + 1;
        }
    }
}
