using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Domain.Entities;
using GenericRepository;
using MediatR;
using TS.Result;


namespace eMuhasebeServer.Application.Features.FileContent2s.SaveAllFileContent2s
{
    internal sealed class SaveAllFileContent2sCommandHandler : IRequestHandler<SaveAllFileContent2sCommand, Result<string>>
    {
        private readonly IFileContent2Repository _fileContent2Repository;
        private readonly IUnitOfWork _unitOfWork;

        public SaveAllFileContent2sCommandHandler(IFileContent2Repository fileContent2Repository, IUnitOfWork unitOfWork)
        {
            _fileContent2Repository = fileContent2Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(SaveAllFileContent2sCommand request, CancellationToken cancellationToken)
        {
            if (request.FileContents.Count == 0)
            {
                return "Güncellenecek FileContent2 kaydı bulunamadı.";
            }

            var fileContentTableRowId = request.FileContents.First().FileContentTableRowId;
            var existingFileContents = await _fileContent2Repository.GetAllByFileContentTableRowIdAsync(fileContentTableRowId, cancellationToken);
            var existingIds = existingFileContents.Select(fc => fc.Id).ToHashSet();

            foreach (var fileContent in request.FileContents)
            {
                if (fileContent.Id.HasValue && existingIds.Contains(fileContent.Id.Value))
                {
                    // Update existing record
                    var existingFileContent = existingFileContents.First(fc => fc.Id == fileContent.Id.Value);
                    existingFileContent.Path = fileContent.Path;
                    existingFileContent.IsActive = fileContent.IsActive;
                    existingFileContent.SortIndex = fileContent.SortIndex;
                    existingFileContent.IsDeleted = false;

                    _fileContent2Repository.Update(existingFileContent);
                    existingIds.Remove(fileContent.Id.Value);
                }
                else
                {
                    // Add new record
                    var newFileContent = new FileContent2
                    {
                        Id = fileContent.Id ?? Guid.NewGuid(),
                        Path = fileContent.Path,
                        IsActive = fileContent.IsActive,
                        SortIndex = fileContent.SortIndex,
                        FileContentTableRowId = fileContentTableRowId,
                        IsDeleted = false
                    };

                    await _fileContent2Repository.AddAsync(newFileContent, cancellationToken);
                }
            }

            // Soft delete records for this FileContentTableRowId that were not in the request
            foreach (var idToDelete in existingIds)
            {
                var fileContentToDelete = existingFileContents.First(fc => fc.Id == idToDelete);
                fileContentToDelete.IsDeleted = true;
                _fileContent2Repository.Update(fileContentToDelete);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "FileContent2 içerikleri başarıyla kaydedildi.";
        }
    }
}