using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities
{
    public sealed class DocumentViewerf1menu7 : Entity
    {
        public string? Konu { get; set; }
        public string? Not { get; set; }
        public DateTime? SaveDateTime { get; set; }
        public string? Filename { get; set; }
        public string? Filesize { get; set; }
        public string? TokenLoaderId { get; set; }
        public string? UserOtherPcLoginSessionId { get; set; }
        public string? Color { get; set; }
        public bool IsActive { get; set; }
        public int SortIndex { get; set; }
    }
}
