
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.CreateSideBarLeft;

public sealed record CreateSideBarLeftCommand : IRequest<Result<string>>
{
    public string? Name { get; init; }
    public bool IsExpanded { get; init; }
    public Guid? ParentId { get; init; }
    public int Order { get; init; }
    public string? IconCss { get; init; }
}