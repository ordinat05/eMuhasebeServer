using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.UpdateSortIndex;

internal sealed class UpdateSortIndexCommandHandler
        : IRequestHandler<UpdateSortIndexCommand, Result<List<FileContentTableRow>>>
{
    private readonly IFileContentTableRowRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSortIndexCommandHandler(
        IFileContentTableRowRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<FileContentTableRow>>> Handle(
        UpdateSortIndexCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Tüm kayıtları tek seferde çekelim
            var allRows = await _repository.GetAllAsync(cancellationToken);
            var updatedRows = new List<FileContentTableRow>();

            foreach (var orderUpdate in request.Orders)
            {
                var row = allRows.FirstOrDefault(r => r.Id == orderUpdate.Id);
                if (row != null)
                {
                    row.Order = orderUpdate.Order;  // SortIndex yerine Order kullanıyoruz
                    await _repository.UpdateAsync(row, cancellationToken);
                    updatedRows.Add(row);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<List<FileContentTableRow>>.Succeed(updatedRows);
        }
        catch (Exception ex)
        {
            return Result<List<FileContentTableRow>>.Failure(ex.Message);
        }
    }
}
