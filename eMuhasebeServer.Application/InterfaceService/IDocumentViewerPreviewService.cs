using eMuhasebeServer.Domain.Entities.Dtos.DocumentViewerDto;

namespace eMuhasebeServer.Application.InterfaceService;

public interface IDocumentViewerPreviewService
{
    //Her bir Endpoint Tablo için burada bir tane yeni oluşturacağız
    Task<DocumentViewerPreviewResponseDto> PreviewDocumentTableDocumentViewersf1menu7Async(DocumentViewerServiceMessageDto previewDto, CancellationToken cancellationToken);

    //Her bir Endpoint Tablo için burada bir tane yeni oluşturacağız

}


