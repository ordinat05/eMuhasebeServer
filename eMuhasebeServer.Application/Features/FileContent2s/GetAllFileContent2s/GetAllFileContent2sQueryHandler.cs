using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Application.Features.FileContent2s.GetAllFileContent2s
{
    internal sealed class GetAllFileContent2sQueryHandler : IRequestHandler<GetAllFileContent2sQuery, Result<List<FileContent2>>>
    {
        private readonly IFileContent2Repository _fileContent2Repository;

        public GetAllFileContent2sQueryHandler(IFileContent2Repository fileContent2Repository)
        {
            _fileContent2Repository = fileContent2Repository;
        }

        public async Task<Result<List<FileContent2>>> Handle(GetAllFileContent2sQuery request, CancellationToken cancellationToken)
        {
            var fileContents = await _fileContent2Repository.GetAllAsync(cancellationToken);
            return fileContents.Where(fc => !fc.IsDeleted).OrderBy(fc => fc.IsDeleted).ToList();
        }
    }
}