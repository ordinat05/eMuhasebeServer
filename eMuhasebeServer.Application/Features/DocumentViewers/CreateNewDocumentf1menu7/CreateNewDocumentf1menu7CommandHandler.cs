using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Entities.Dtos;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace eMuhasebeServer.Application.Features.DocumentViewers.CreateNewDocumentf1menu7
{
    public sealed class CreateNewDocumentf1menu7CommandHandler
            : IRequestHandler<CreateNewDocumentf1menu7Command, Response<CreateNewDocumentf1menu7Dto>>
    {
        private readonly IDocumentViewerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public CreateNewDocumentf1menu7CommandHandler(
            IDocumentViewerRepository repository,
            IUnitOfWork unitOfWork, 
            IFileService fileService,
            IConfiguration configuration)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _configuration = configuration;
        }

        public async Task<Response<CreateNewDocumentf1menu7Dto>> Handle(
            CreateNewDocumentf1menu7Command request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Önce dosyayı taşımayı dene
                try
                {
                    await _fileService.MoveToBasePath7Async(request.Id.ToString(), cancellationToken);
                }
                catch (Exception ex)
                {
                    return Response<CreateNewDocumentf1menu7Dto>.Fail(
                        $"Dosya taşıma işlemi başarısız oldu: {ex.Message}",
                        500,
                        true);
                }

                // Dosya başarıyla taşındıysa veritabanı işlemlerine devam et
                var documentViewerf1menu7 = new DocumentViewerf1menu7
                {
                    Id = request.Id,
                    Konu = request.Konu,
                    Not = request.Not,
                    SaveDateTime = request.SaveDateTime,
                    Filename = request.Filename,
                    Filesize = request.Filesize,
                    TokenLoaderId = request.TokenLoaderId,
                    UserOtherPcLoginSessionId = request.UserOtherPcLoginSessionId,
                    IsActive = request.IsActive,
                    IsDeleted = request.IsDeleted,
                    SortIndex = request.SortIndex
                };

                await _repository.AddAsync(documentViewerf1menu7, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Response<CreateNewDocumentf1menu7Dto>.Success(new CreateNewDocumentf1menu7Dto
                {
                    Id = documentViewerf1menu7.Id,
                    Konu = documentViewerf1menu7.Konu,
                    Not = documentViewerf1menu7.Not,
                    SaveDateTime = documentViewerf1menu7.SaveDateTime,
                    Filename = documentViewerf1menu7.Filename,
                    Filesize = documentViewerf1menu7.Filesize,
                    TokenLoaderId = documentViewerf1menu7.TokenLoaderId,
                    UserOtherPcLoginSessionId = documentViewerf1menu7.UserOtherPcLoginSessionId,
                    IsActive = documentViewerf1menu7.IsActive,
                    IsDeleted = documentViewerf1menu7.IsDeleted,
                    SortIndex = documentViewerf1menu7.SortIndex
                }, 200);
            }
            catch (Exception ex)
            {
                return Response<CreateNewDocumentf1menu7Dto>.Fail(
                    $"Belge kaydedilirken bir hata oluştu: {ex.Message}",
                    500,
                    true);
            }
        }
    }
}
