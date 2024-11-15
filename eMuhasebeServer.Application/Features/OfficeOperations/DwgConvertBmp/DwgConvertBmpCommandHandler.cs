using eMuhasebeServer.Application.Features.DropZones.DropZonePreviewDwg;
using eMuhasebeServer.Application.InterfaceService;
using MediatR;
using TS.Result;
using UtilitiesNet6Framework.dwgimageexport;
using eMuhasebeServer.Domain.Utilities.FileStorageBasePath;


namespace eMuhasebeServer.Application.Features.OfficeOperations.DwgConvertBmp;

internal sealed class DwgConvertBmpCommandHandler : IRequestHandler<DwgConvertBmpCommand, Result<string>>
{
    private readonly IFileService _fileService;

    public DwgConvertBmpCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<Result<string>> Handle(DwgConvertBmpCommand request, CancellationToken cancellationToken)
    {
    //C: \Users\Lenovo\source\repos\ProinsSolutions\eMuhasebeServer\eMuhasebeServer.Application\Features\DropZones\DropZonePreviewDwg\DropZonePreviewDwgCommand.cs Burada zaten var. Bu yedek
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