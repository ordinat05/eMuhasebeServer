using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Entities.Dtos;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;

namespace eMuhasebeServer.Application.Features.DocumentViewers.UpdateSortIndexf1menu7;


public sealed class UpdateSortIndexf1menu7CommandHandler :
    IRequestHandler<UpdateSortIndexf1menu7Command, Response<List<DocumentViewerf1menu7>>>
{
    private readonly IDocumentViewerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSortIndexf1menu7CommandHandler(
        IDocumentViewerRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<List<DocumentViewerf1menu7>>> Handle(
        UpdateSortIndexf1menu7Command request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Tüm kayıtları getir
            var allRecords = await _repository.GetAllAsync(cancellationToken);
            var updatedRecords = new List<DocumentViewerf1menu7>();

            // Her bir sıralama güncellemesi için işlem yap
            foreach (var sortIndex in request.SortIndexs)
            {
                var record = allRecords.FirstOrDefault(r => r.Id == sortIndex.Id);
                if (record != null)
                {
                    record.SortIndex = sortIndex.SortIndex;
                    await _repository.UpdateAsync(record, cancellationToken);
                    updatedRecords.Add(record);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Güncellenmiş kayıtları sıralı şekilde döndür
            var sortedRecords = updatedRecords.OrderBy(r => r.SortIndex).ToList();

            return Response<List<DocumentViewerf1menu7>>.Success(sortedRecords, 200);
        }
        catch (Exception ex)
        {
            return Response<List<DocumentViewerf1menu7>>.Fail(
                $"Sıralama güncellenirken bir hata oluştu: {ex.Message}",
                500,
                true);
        }
    }
}