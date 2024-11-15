namespace eMuhasebeServer.Domain.Entities.Dtos.DocumentViewerDto;

public class DocumentViewerServiceMessageDto
{
    public Guid Id { get; set; }
    public string? Opener { get; set; }
    public string? DbTableName { get; set; }
    public string? SourceFilePathName1 { get; set; }
    public string? SourceFilePathName2 { get; set; }
    public string? CopyTargetFilePath { get; set; }
    public string? PresentationFilePath { get; set; }

}


