namespace eMuhasebeServer.Domain.Entities.Dtos.DocumentViewerDto;

public class DocumentViewerPreviewResponseDto
{
    public string? Opener { get; set; }
    public string PreviewUrl { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string DestinationPath { get; set; } = string.Empty;
}