using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContent2s.GetAllByFileContentTableRowId;


internal sealed class GetAllByFileContentTableRowIdCommandHandler : IRequestHandler<GetAllByFileContentTableRowIdCommand, Result<List<FileContent2>>>
{
    private readonly IFileContent2Repository _fileContent2Repository;

    public GetAllByFileContentTableRowIdCommandHandler(IFileContent2Repository fileContent2Repository)
    {
        _fileContent2Repository = fileContent2Repository;
    }

    public async Task<Result<List<FileContent2>>> Handle(GetAllByFileContentTableRowIdCommand request, CancellationToken cancellationToken)
    {
        var fileContents = await _fileContent2Repository.GetAllAsync(cancellationToken);
        return fileContents.Where(fc => fc.FileContentTableRowId == request.fileContentTableRowId).ToList();
    }
}