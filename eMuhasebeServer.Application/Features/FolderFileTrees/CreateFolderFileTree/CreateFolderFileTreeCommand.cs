using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FolderFileTrees.CreateFolderFileTree;

public sealed record CreateFolderFileTreeCommand : IRequest<Result<string>>
{
    public string? Name { get; init; }
    public bool IsExpanded { get; init; }
    public Guid? ParentId { get; init; }
    public int Order { get; init; }
    public string? IconCss { get; init; }
    public bool IsDirectory { get; init; }
    public long Size { get; init; }
}
