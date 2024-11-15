using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.UpdateFileContentTableRow
{
    internal sealed class UpdateFileContentTableRowCommandHandler : IRequestHandler<UpdateFileContentTableRowCommand, Result<FileContentTableRow>>
    {
        private readonly IFileContentTableRowRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFileContentTableRowCommandHandler(IFileContentTableRowRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FileContentTableRow>> Handle(UpdateFileContentTableRowCommand request, CancellationToken cancellationToken)
        {
            var existingRow = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (existingRow == null)
            {
                return Result<FileContentTableRow>.Failure($"FileContentTableRow ID {request.Id} bulunamadı.");
            }

            existingRow.IsActive = request.IsActive;
            existingRow.Konu = request.Konu;
            existingRow.Not = request.Not;
            existingRow.ToggleButtonState = request.ToggleButtonState;
            existingRow.SaveDateTime = request.SaveDateTime;
            existingRow.FileCount = request.FileCount;
            existingRow.GoToLink = request.GoToLink;
            existingRow.Order = request.Order;
            existingRow.Status = request.Status;  
            existingRow.Color = request.Color;   

            await _repository.UpdateAsync(existingRow, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<FileContentTableRow>.Succeed(existingRow);
        }
    }
}