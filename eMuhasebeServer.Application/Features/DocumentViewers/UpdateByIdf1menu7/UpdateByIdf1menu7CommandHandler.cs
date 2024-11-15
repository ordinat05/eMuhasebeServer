using eMuhasebeServer.Domain.Entities.Dtos;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;

namespace eMuhasebeServer.Application.Features.DocumentViewers.UpdateByIdf1menu7;

public sealed class UpdateByIdf1menu7CommandHandler :
      IRequestHandler<UpdateByIdf1menu7Command, Response<UpdateByIdf1menu7Dto>>
{
    private readonly IDocumentViewerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateByIdf1menu7CommandHandler(
        IDocumentViewerRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<UpdateByIdf1menu7Dto>> Handle(
        UpdateByIdf1menu7Command request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Response<UpdateByIdf1menu7Dto>.Fail(
                    "Kayıt bulunamadı",
                    404,
                    true);
            }

            // Gelen değerlerle entity'yi güncelle
            entity.Konu = request.Konu;
            entity.Not = request.Not;
            entity.SaveDateTime = request.SaveDateTime;
            entity.Filename = request.Filename;
            entity.Filesize = request.Filesize;
            entity.TokenLoaderId = request.TokenLoaderId;
            entity.UserOtherPcLoginSessionId = request.UserOtherPcLoginSessionId;
            entity.Color = request.Color;
            entity.IsActive = request.IsActive;
            entity.SortIndex = request.SortIndex;

            await _repository.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new UpdateByIdf1menu7Dto
            {
                Id = entity.Id,
                Konu = entity.Konu,
                Not = entity.Not,
                SaveDateTime = entity.SaveDateTime,
                Filename = entity.Filename,
                Filesize = entity.Filesize,
                TokenLoaderId = entity.TokenLoaderId,
                UserOtherPcLoginSessionId = entity.UserOtherPcLoginSessionId,
                Color = entity.Color,
                IsActive = entity.IsActive,
                SortIndex = entity.SortIndex
            };

            return Response<UpdateByIdf1menu7Dto>.Success(dto, 200);
        }
        catch (Exception ex)
        {
            return Response<UpdateByIdf1menu7Dto>.Fail(
                $"Güncelleme sırasında bir hata oluştu: {ex.Message}",
                500,
                true);
        }
    }
}