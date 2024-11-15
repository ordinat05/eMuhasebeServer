using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.GetAllSideBarLeft;

public sealed class GetAllSideBarLeftQueryHandler(
    ISideBarLeftRepository sideBarLeftRepository
    ) : IRequestHandler<GetAllSideBarLeftQuery, Result<List<SideBarLeft>>>
{
    public async Task<Result<List<SideBarLeft>>> Handle(GetAllSideBarLeftQuery request, CancellationToken cancellationToken)
    {
        List<SideBarLeft> sideBarLeft =
            await sideBarLeftRepository
                .GetAll()
                .Where(p => !p.IsDeleted)
                .ToListAsync(cancellationToken);

        return Result<List<SideBarLeft>>.Succeed(sideBarLeft);
    }
}
