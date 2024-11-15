public class HtmlConversionRequestDto
{
    public string? CatchSourceFile { get; set; }          // Kaynak dosyanın tam yolu
    public string? FileNameAndExtension { get; set; }     // Dosya adı ve uzantısı
    public string? CopyTargetFilePath { get; set; }       // Hedef klasör yolu
    public string? NewFolderAndFileName { get; set; }     // Yeni klasör ve dosya adı
    public string? MachineId { get; set; }     // Yeni klasör ve dosya adı
}