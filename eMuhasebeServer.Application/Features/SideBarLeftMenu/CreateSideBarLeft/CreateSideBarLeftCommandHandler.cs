using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.CreateSideBarLeft;

internal sealed class CreateSideBarLeftCommandHandler : IRequestHandler<CreateSideBarLeftCommand, Result<string>>
{
    private readonly ISideBarLeftRepository _sideBarLeftRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSideBarLeftCommandHandler(
        ISideBarLeftRepository sideBarLeftRepository,
        IUnitOfWork unitOfWork)
    {
        _sideBarLeftRepository = sideBarLeftRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateSideBarLeftCommand request, CancellationToken cancellationToken)
    {
        bool nameExists = await _sideBarLeftRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);
        if (nameExists)
        {
            return Result<string>.Failure("Bu *Menü* daha önce kaydedilmiş");
        }

        // ParentId'si null olan kayıtlar arasındaki en yüksek Order numarasını bul
        int maxOrder = await _sideBarLeftRepository
            .Where(p => p.ParentId == null)
            .MaxAsync(p => (int?)p.Order) ?? 0;

        var newSideBarLeft = new SideBarLeft
        {
            Name = request.Name,
            IsExpanded = request.IsExpanded,
            ParentId = request.ParentId,
            Order = maxOrder + 1,
            IconCss = request.IconCss
        };

        await _sideBarLeftRepository.AddAsync(newSideBarLeft, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("SideBar başarıyla oluşturuldu");
    }
}
