using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Entities.Dtos.DocumentViewerDto;

namespace eMuhasebeServer.Application.InterfaceService
{
    public interface IDocumentViewerProcessingService
    {
        // Dosya HTM ise yada microsoft - google ise ona göre iframe üretmesi planlanıyordu ama iframe üretilmeyecek.
        // Burada office dosyası HTM e dönüştürülecek. Oluşturulmuş olan path+Filename&Extension RETURN edilecek.
        // Burada office dosyası CopyTargetFilePath adresine kopyalanacak. 
        string GetBasePath();
        string GetBasePath5();
        string GetBasePath7();
        string GetBasePath8();

        Task<DocumentViewerPreviewResponseDto> ProcessXlsHtmAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessDocHtmAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessXlsGoogleAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessXlsMicrosoftAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessXlsxGoogleAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessXlsxMicrosoftAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessDocGoogleAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessDocMicrosoftAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessDocxGoogleAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessDocxMicrosoftAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessPngAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessBmpAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessJpgAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessJpegAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessXpsAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessPdfAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessTxtAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);
        Task<DocumentViewerPreviewResponseDto> ProcessLspAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document);

    }
}
