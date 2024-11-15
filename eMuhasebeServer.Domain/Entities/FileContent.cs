using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities
{
    public sealed class FileContent : Entity
    {
        public string? Path { get; set; }
        public bool IsActive { get; set; }
    }
}
