using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContent2s.DeleteFileContent2sById;

internal sealed class DeleteFileContent2ByIdCommandHandler : IRequestHandler<DeleteFileContent2ByIdCommand, Result<string>>
{
    private readonly IFileContent2Repository _fileContent2Repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFileContent2ByIdCommandHandler(IFileContent2Repository fileContent2Repository, IUnitOfWork unitOfWork)
    {
        _fileContent2Repository = fileContent2Repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteFileContent2ByIdCommand request, CancellationToken cancellationToken)
    {
        await _fileContent2Repository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("FileContent2 başarıyla silindi.");
    }
}