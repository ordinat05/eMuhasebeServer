using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContents.DeleteFileContentById;

internal sealed class DeleteFileContentByIdCommandHandler : IRequestHandler<DeleteFileContentByIdCommand, Result<string>>
{
    private readonly IFileContentRepository _fileContentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFileContentByIdCommandHandler(IFileContentRepository fileContentRepository, IUnitOfWork unitOfWork)
    {
        _fileContentRepository = fileContentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteFileContentByIdCommand request, CancellationToken cancellationToken)
    {
        await _fileContentRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Dosya içeriği başarıyla silindi.";
    }
}
