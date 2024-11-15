using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Entities.Dtos.DocumentViewerDto;

namespace eMuhasebeServer.Infrastructure.Services;

public class DocumentViewerPreviewService : IDocumentViewerPreviewService
{
    private readonly IFindIdinTheTableService _findIdinTheTableService;
    private readonly IDocumentViewerProcessingService _processingService;

    public DocumentViewerPreviewService(
        IFindIdinTheTableService findIdinTheTableService,
        IDocumentViewerProcessingService processingService)
    {
        _findIdinTheTableService = findIdinTheTableService;
        _processingService = processingService;
    }
    public async Task<DocumentViewerPreviewResponseDto> PreviewDocumentTableDocumentViewersf1menu7Async(DocumentViewerServiceMessageDto previewDto, CancellationToken cancellationToken)
    {
        try
        {
            var document = await _findIdinTheTableService.GetEntityByTableDocumentViewersf1menu7(
                previewDto.DbTableName!,
                previewDto.Id,
                cancellationToken)
                ?? throw new Exception($"Belge bulunamadı. ID: {previewDto.Id}, Tablo: {previewDto.DbTableName}");

            dynamic dynamicTableEntity = document;

            if (string.IsNullOrEmpty((string?)dynamicTableEntity.Filename))
            {
                throw new Exception("Dosya adı boş olamaz");
            }

            string fileExtension = Path.GetExtension((string)dynamicTableEntity.Filename).ToLower();

            return fileExtension switch
            {
                // Resim dosyaları
                ".png" => await _processingService.ProcessPngAsync(previewDto, dynamicTableEntity),
                ".bmp" => await _processingService.ProcessBmpAsync(previewDto, dynamicTableEntity),
                ".jpg" => await _processingService.ProcessJpgAsync(previewDto, dynamicTableEntity),
                ".jpeg" => await _processingService.ProcessJpegAsync(previewDto, dynamicTableEntity),
                ".xps" => await _processingService.ProcessXpsAsync(previewDto, dynamicTableEntity),
                ".pdf" => await _processingService.ProcessPdfAsync(previewDto, dynamicTableEntity),
                ".txt" => await _processingService.ProcessTxtAsync(previewDto, dynamicTableEntity),
                ".lsp" => await _processingService.ProcessLspAsync(previewDto, dynamicTableEntity),

                // Office dosyaları - Opener değerine göre farklı işlemler
                ".xls" => previewDto.Opener switch
                {
                    "0" => await _processingService.ProcessXlsHtmAsync(previewDto, dynamicTableEntity),
                    "1" => await _processingService.ProcessXlsGoogleAsync(previewDto, dynamicTableEntity),
                    "2" => await _processingService.ProcessXlsMicrosoftAsync(previewDto, dynamicTableEntity),
                    _ => throw new ArgumentException($"Desteklenmeyen görüntüleyici türü: {previewDto.Opener}")
                },
                ".xlsx" => previewDto.Opener switch
                {
                    "0" => await _processingService.ProcessXlsHtmAsync(previewDto, dynamicTableEntity),
                    "1" => await _processingService.ProcessXlsxGoogleAsync(previewDto, dynamicTableEntity),
                    "2" => await _processingService.ProcessXlsxMicrosoftAsync(previewDto, dynamicTableEntity),
                    _ => throw new ArgumentException($"Desteklenmeyen görüntüleyici türü: {previewDto.Opener}")
                },
                ".doc" => previewDto.Opener switch
                {
                    "0" => await _processingService.ProcessDocHtmAsync(previewDto, dynamicTableEntity),
                    "1" => await _processingService.ProcessDocGoogleAsync(previewDto, dynamicTableEntity),
                    "2" => await _processingService.ProcessDocMicrosoftAsync(previewDto, dynamicTableEntity),
                    _ => throw new ArgumentException($"Desteklenmeyen görüntüleyici türü: {previewDto.Opener}")
                },
                ".docx" => previewDto.Opener switch
                {
                    "0" => await _processingService.ProcessDocHtmAsync(previewDto, dynamicTableEntity),
                    "1" => await _processingService.ProcessDocxGoogleAsync(previewDto, dynamicTableEntity),
                    "2" => await _processingService.ProcessDocxMicrosoftAsync(previewDto, dynamicTableEntity),
                    _ => throw new ArgumentException($"Desteklenmeyen görüntüleyici türü: {previewDto.Opener}")
                },
                _ => throw new NotSupportedException($"Desteklenmeyen dosya uzantısı: {fileExtension}")
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Önizleme URL'i oluşturulurken hata oluştu: {ex.Message}", ex);
        }

        //string test = "testtt";
        //    return await Task.FromResult(test);
        //}
        //catch (Exception)
        //{
        //    string test = "testtt";
        //    return await Task.FromResult(test);
        //}
    }
}

