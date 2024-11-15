using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Entities.Dtos;
using MediatR;


namespace eMuhasebeServer.Application.Features.DocumentViewers.SaveDropZoneOneFiles;

public class SaveDropZoneOneFileCommandHandler : IRequestHandler<SaveDropZoneOneFileCommand, Response<SaveDropZoneOneFileDto>>
{
    private readonly IFileService _fileService;

    public SaveDropZoneOneFileCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    // Dosya Yükleme Silme Çalışıyor Versiyon 1 START
    #region Dosya Yükleme Silme Çalışıyor Versiyon 1
    public async Task<Response<SaveDropZoneOneFileDto>> Handle(SaveDropZoneOneFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.File == null)
            {
                return Response<SaveDropZoneOneFileDto>.Fail("Dosya bulunamadı.", 400, true);
            }

            var (folderPathAndFileName, fileNameAndFileExtension, _, _) = await _fileService.DropZoneOneFileDetect(request.File, request.FileGuidId!, cancellationToken);


            var fileSize = request.File.Length > 1048576
                ? $"{request.File.Length / 1048576.0:F2}MB"
                : $"{request.File.Length / 1024.0:F2}KB";

            var responseData = new SaveDropZoneOneFileDto
            {
                Filename = request.File.FileName,
                Filesize = fileSize,
                FileGuidId = request.FileGuidId,
                UserOtherPcLoginSessionId = request.UserOtherPcLoginSessionId,
                TokenLoaderId = request.TokenLoaderId,
                FilePath = folderPathAndFileName
            };

            return Response<SaveDropZoneOneFileDto>.Success(responseData, 200);
        }
        catch (Exception ex)
        {
            return Response<SaveDropZoneOneFileDto>.Fail($"Dosya kaydedilirken bir hata oluştu: {ex.Message}", 500, true);
        }
    }
    #endregion 
    // Dosya Yükleme Silme Çalışıyor Versiyon 1 END
}