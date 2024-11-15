using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Entities.Dtos;
using MediatR;

namespace eMuhasebeServer.Application.Features.DocumentViewers.SaveAllDropZoneFiles;

public class SaveAllDropZoneFileCommandHandler : IRequestHandler<SaveAllDropZoneFileCommand, Response<SaveAllDropZoneFileDto>>
{
    private readonly IFileService _fileService;

    public SaveAllDropZoneFileCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<Response<SaveAllDropZoneFileDto>> Handle(SaveAllDropZoneFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.File == null)
            {
                return Response<SaveAllDropZoneFileDto>.Fail("Dosya bulunamadı.", 400, true);
            }

            var (_, fileNameAndFileExtension, _, fileGuidIdName) = await _fileService.UploadDocumentViewerFileAsync(request.File, cancellationToken);


            var fileSize = request.File.Length > 1048576
                ? $"{request.File.Length / 1048576.0:F2}MB"
                : $"{request.File.Length / 1024.0:F2}KB";

            var responseData = new SaveAllDropZoneFileDto
            {
                Name = "file",
                Filename = request.File.FileName,
                Filesize = fileSize,
                GuidId = fileGuidIdName
            };

            return Response<SaveAllDropZoneFileDto>.Success(responseData, 200);
        }
        catch (Exception ex)
        {
            return Response<SaveAllDropZoneFileDto>.Fail($"Dosya kaydedilirken bir hata oluştu: {ex.Message}", 500, true);
        }
    }
}