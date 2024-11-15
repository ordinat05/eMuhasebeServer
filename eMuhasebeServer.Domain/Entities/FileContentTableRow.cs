using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities
{
    public sealed class FileContentTableRow : Entity
    {
        public string? Konu { get; set; }
        public string? Not { get; set; }
        public string? ToggleButtonState { get; set; }
        public DateTime? SaveDateTime { get; set; }
        public string? FileCount { get; set; }
        public string? GoToLink { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public string? Status { get; set; }  
        public string? Color { get; set; }   
        public List<FileContent2>? FileContent2ler { get; set; }
    }
}
