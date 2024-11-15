using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.DeleteFileContentTableRowById
{
    internal sealed class DeleteFileContentTableRowByIdCommandHandler : IRequestHandler<DeleteFileContentTableRowByIdCommand, Result<Unit>>
    {
        private readonly IFileContentTableRowRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFileContentTableRowByIdCommandHandler(IFileContentTableRowRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteFileContentTableRowByIdCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteByIdAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}