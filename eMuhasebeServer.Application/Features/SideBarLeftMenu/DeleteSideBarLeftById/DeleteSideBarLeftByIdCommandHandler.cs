using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.DeleteSideBarLeftById;

internal sealed class DeleteSideBarLeftByIdCommandHandler(
    ISideBarLeftRepository sideBarLeftRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteSideBarLeftByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteSideBarLeftByIdCommand request, CancellationToken cancellationToken)
    {
        SideBarLeft sideBarLeft = await sideBarLeftRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (sideBarLeft is null)
        {
            return Result<string>.Failure("Menü Bulunamadı");
        }
        sideBarLeft.IsDeleted = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Şirket başarıyla silindi";
    }
}
