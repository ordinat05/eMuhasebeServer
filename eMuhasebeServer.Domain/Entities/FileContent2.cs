using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities
{
    public sealed class FileContent2 : Entity
    {
        public Guid FileContentTableRowId { get; set; }
        public string? Path { get; set; }
        public bool IsActive { get; set; }
        public int SortIndex { get; set; }
    }
}
