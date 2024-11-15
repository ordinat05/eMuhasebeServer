using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContents.GetAllFileContents
{
    internal sealed class GetAllFileContentsQueryHandler : IRequestHandler<GetAllFileContentsQuery, Result<List<FileContent>>>
    {
        private readonly IFileContentRepository _fileContentRepository;

        public GetAllFileContentsQueryHandler(IFileContentRepository fileContentRepository)
        {
            _fileContentRepository = fileContentRepository;
        }

        public async Task<Result<List<FileContent>>> Handle(GetAllFileContentsQuery request, CancellationToken cancellationToken)
        {
            var fileContents = await _fileContentRepository.GetAllAsync(cancellationToken);
            return fileContents.Where(fc => !fc.IsDeleted).ToList();
        }
    }
}
