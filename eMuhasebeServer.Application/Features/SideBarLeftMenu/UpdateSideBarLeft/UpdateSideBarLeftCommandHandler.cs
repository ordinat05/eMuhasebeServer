using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.UpdateSideBarLeft;


internal sealed class UpdateSideBarLeftCommandHandler : IRequestHandler<UpdateSideBarLeftCommand, Result<string>>
{
    private readonly ISideBarLeftRepository _sideBarLeftRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSideBarLeftCommandHandler(ISideBarLeftRepository sideBarLeftRepository, IUnitOfWork unitOfWork)
    {
        _sideBarLeftRepository = sideBarLeftRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateSideBarLeftCommand request, CancellationToken cancellationToken)
    {
        var sideBarLeft = await _sideBarLeftRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sideBarLeft == null)
        {
            return Result<string>.Failure("Menü bulunamadı");
        }

        sideBarLeft.Name = request.Name;
        sideBarLeft.IconCss = request.IconCss;
        sideBarLeft.IsExpanded = request.IsExpanded;
        sideBarLeft.ParentId = request.ParentId;
        sideBarLeft.Order = request.Order;

        await _sideBarLeftRepository.UpdateAsync(sideBarLeft);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Menü başarıyla güncellendi");
    }


    private int UpdateMenuTreeRecursive(SideBarLeft currentMenu, UpdateSideBarLeftCommand command, Guid? parentId, int order)
    {
        currentMenu.Name = command.Name;
        currentMenu.IsExpanded = command.IsExpanded;
        currentMenu.ParentId = parentId;
        currentMenu.Order = order;

        int nextOrder = order + 1;

        if (command.Children != null && command.Children.Any())
        {
            foreach (var childCommand in command.Children)
            {
                var childMenu = currentMenu.Children.FirstOrDefault(c => c.Id == childCommand.Id) ?? new SideBarLeft();
                nextOrder = UpdateMenuTreeRecursive(childMenu, childCommand, currentMenu.Id, nextOrder);
                if (!currentMenu.Children.Contains(childMenu))
                {
                    currentMenu.Children.Add(childMenu);
                }
            }

            // Artık mevcut olmayan alt menüleri kaldır
            var childrenToRemove = currentMenu.Children.Where(c => !command.Children.Any(cc => cc.Id == c.Id)).ToList();
            foreach (var childToRemove in childrenToRemove)
            {
                childToRemove.IsDeleted = true;
            }
        }
        else
        {
            // Tüm alt menüleri kaldır
            foreach (var child in currentMenu.Children)
            {
                child.IsDeleted = true;
            }
        }

        return nextOrder;
    }
}

