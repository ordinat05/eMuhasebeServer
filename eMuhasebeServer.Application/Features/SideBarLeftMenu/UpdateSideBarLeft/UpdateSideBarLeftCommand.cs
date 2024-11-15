
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.UpdateSideBarLeft;

public sealed record UpdateSideBarLeftCommand : IRequest<Result<string>>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? IconCss { get; init; }

    public bool IsExpanded { get; init; }
    public Guid? ParentId { get; init; }
    public int Order { get; init; }


    public List<UpdateSideBarLeftCommand>? Children { get; init; }
}
