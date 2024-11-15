using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public class FolderFileTree : Entity
{
    public string Name { get; set; } = string.Empty;
    public bool IsExpanded { get; set; }
    public Guid? ParentId { get; set; }
    public int Order { get; set; }
    public string IconCss { get; set; } = string.Empty;
    public FolderFileTree? ParentNode { get; set; }
    public List<FolderFileTree> Children { get; set; } = new List<FolderFileTree>();
    public bool IsDirectory { get; set; }
    public long Size { get; set; }

    public FolderFileTree()
    {
        IsExpanded = true;
        IconCss = IsDirectory ? "folder-icon" : "file-icon";
    }
}
