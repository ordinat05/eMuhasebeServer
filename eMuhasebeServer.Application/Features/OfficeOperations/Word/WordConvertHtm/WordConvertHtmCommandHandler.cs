using eMuhasebeServer.Application.InterfaceService;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.OfficeOperations.Word.WordConvertHtm;

internal sealed class WordConvertHtmCommandHandler : IRequestHandler<WordConvertHtmCommand, Result<string>>
{
    private readonly IWordService _wordService;

    public WordConvertHtmCommandHandler(IWordService wordService)
    {
        _wordService = wordService;
    }

    public Task<Result<string>> Handle(WordConvertHtmCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //var baseDirectory = Path.GetDirectoryName(request.FolderPathAndFileName);


            if (string.IsNullOrEmpty(request.UploadPath))
            {
                return Task.FromResult(Result<string>.Failure("Geçersiz dosya yolu. Dosya Upload Başarısız."));
            }
            // Dosyanın bulunduğu dizini alır. Eğer geçersizse, hata döndürür.

            var htmOutputDirectory = _wordService.ConvertWordToHtmlSrv(request.FolderPathAndFileName, request.FileNameAndFileExtension, request.UploadPath, request.FileGuidIdName);
            // Burada Sonuç Güzel htmOutputDirectory = "C:\\ArsivDevelopment\\tammerkezFileStock\\BackendFiles\\ExcelInterop\\excel_uploads\\1f1c1c63-9c7a-4d4c-883d-959ba5868622\\1f1c1c63-9c7a-4d4c-883d-959ba5868622.htm"
            if (string.IsNullOrEmpty(htmOutputDirectory))
            {
                return Task.FromResult(Result<string>.Failure("Word dosyası HTML'e dönüştürülemedi."));
            }

            //var htmlFilePath = Path.Combine(htmOutputDirectory, Path.GetFileNameWithoutExtension(request.FileNameAndFileExtension) + ".htm");

            if (File.Exists(htmOutputDirectory))
            {
                return Task.FromResult(Result<string>.Succeed(htmOutputDirectory));
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