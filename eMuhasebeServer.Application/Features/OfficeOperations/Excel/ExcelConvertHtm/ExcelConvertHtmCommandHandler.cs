using eMuhasebeServer.Application.InterfaceService;
using MediatR;
using TS.Result;


namespace eMuhasebeServer.Application.Features.OfficeOperations.Excel.ExcelConvertHtm;

internal sealed class ExcelConvertHtmCommandHandler : IRequestHandler<ExcelConvertHtmCommand, Result<string>>
{
   
    private readonly IExcelService _excelService;

    public ExcelConvertHtmCommandHandler( IExcelService excelService)
    {
        _excelService = excelService;
    }

    public Task<Result<string>> Handle(ExcelConvertHtmCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //var baseDirectory = Path.GetDirectoryName(request.FolderPathAndFileName);
            

            if (string.IsNullOrEmpty(request.UploadPath))
            {
                return Task.FromResult(Result<string>.Failure("Geçersiz dosya yolu. Dosya Upload Başarısız."));
            }
            // Dosyanın bulunduğu dizini alır. Eğer geçersizse, hata döndürür.

            var htmOutputDirectory = _excelService.ConvertExcelToHtmlSrv(request.FolderPathAndFileName, request.FileNameAndFileExtension, request.UploadPath, request.FileGuidIdName);

            if (string.IsNullOrEmpty(htmOutputDirectory))
            {
                return Task.FromResult(Result<string>.Failure("Excel dosyası HTML'e dönüştürülemedi."));
            }

         var htmlFilePath = Path.Combine(htmOutputDirectory, Path.GetFileNameWithoutExtension(request.FileNameAndFileExtension) + ".htm");

            if (File.Exists(htmlFilePath))
            {
                return Task.FromResult(Result<string>.Succeed(htmlFilePath));
            }
            else
            {
                return Task.FromResult(Result<string>.Failure("HTML dosyası oluşturulamadı."));
            }
            // HTML dosyası oluşturulduysa başarı mesajı, oluşturulmadıysa hata mesajı döndürür.

        }
        catch (Exception ex)
        {
            return Task.FromResult(Result<string>.Failure($"Dönüştürme sırasında bir hata oluştu: {ex.Message}"));
        }
    }
}
