using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FolderFileTrees.UpdateFolderFileTree;

public sealed record UpdateFolderFileTreeCommand : IRequest<Result<string>>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public bool IsExpanded { get; init; }
    public Guid? ParentId { get; init; }
    public int Order { get; init; }
    public string? IconCss { get; init; }
    public bool IsDirectory { get; init; }
    public long Size { get; init; }
    public List<UpdateFolderFileTreeCommand>? Children { get; init; }
}