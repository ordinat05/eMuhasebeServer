using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.CreateFileContentTableRow
{
    internal sealed class CreateFileContentTableRowCommandHandler : IRequestHandler<CreateFileContentTableRowCommand, Result<FileContentTableRow>>
    {
        private readonly IFileContentTableRowRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFileContentTableRowCommandHandler(IFileContentTableRowRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FileContentTableRow>> Handle(CreateFileContentTableRowCommand request, CancellationToken cancellationToken)
        {
            var fileContentTableRow = new FileContentTableRow
            {
                Konu = request.Konu,
                Not = request.Not,
                ToggleButtonState = request.ToggleButtonState,
                SaveDateTime = request.SaveDateTime,
                GoToLink = request.GoToLink,
                FileCount = request.FileCount,
                IsActive = request.IsActive,
                Order = request.Order,
                IsDeleted = request.IsDeleted,
                Status = request.Status,  
                Color = request.Color,    
                FileContent2ler = new List<FileContent2>() // Boş liste oluşturuyoruz
            };

            await _repository.AddAsync(fileContentTableRow, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<FileContentTableRow>.Succeed(fileContentTableRow);
        }
    }
}