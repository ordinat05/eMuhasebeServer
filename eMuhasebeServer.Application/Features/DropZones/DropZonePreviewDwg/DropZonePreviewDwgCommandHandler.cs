using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Utilities.FileStorageBasePath;
using MediatR;
using TS.Result;
using UtilitiesNet6Framework.dwgimageexport;

namespace eMuhasebeServer.Application.Features.DropZones.DropZonePreviewDwg;

internal sealed class DropZonePreviewDwgCommandHandler : IRequestHandler<DropZonePreviewDwgCommand, Result<string>>
{
    private readonly IFileService _fileService;

    public DropZonePreviewDwgCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<Result<string>> Handle(DropZonePreviewDwgCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //var basePath = _fileService.GetBasePath();
            var basePath = FileStorageBasePaths.BasePath;
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
            var filePath = Path.Combine(basePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }

            var bitmapPath = DwgSnapshotsHelper.Snapshot(filePath);

            if (string.IsNullOrEmpty(bitmapPath))
            {
                return Result<string>.Failure("DWG dosyasından bitmap oluşturulamadı.");
            }

            // Bitmap dosyasını oku ve base64'e çevir
            byte[] imageArray = await File.ReadAllBytesAsync(bitmapPath, cancellationToken);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            // Orijinal DWG dosyasını sil (isteğe bağlı)
            // File.Delete(filePath);

            return Result<string>.Succeed(base64ImageRepresentation);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Dosya işlenirken bir hata oluştu: {ex.Message}");
        }
    }
}