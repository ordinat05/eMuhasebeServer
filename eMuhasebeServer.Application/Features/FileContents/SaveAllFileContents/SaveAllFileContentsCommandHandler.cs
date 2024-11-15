using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Domain.Entities;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContents.SaveAllFileContents
{
    internal sealed class SaveAllFileContentsCommandHandler : IRequestHandler<SaveAllFileContentsCommand, Result<string>>
    {
        private readonly IFileContentRepository _fileContentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SaveAllFileContentsCommandHandler(IFileContentRepository fileContentRepository, IUnitOfWork unitOfWork)
        {
            _fileContentRepository = fileContentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(SaveAllFileContentsCommand request, CancellationToken cancellationToken)
        {
            await _fileContentRepository.DeleteAllAsync(cancellationToken);

            foreach (var fileContent in request.FileContents)
            {
                if (fileContent.Id == null || fileContent.Id == Guid.Empty)
                {
                    // Yeni kayıt
                    await _fileContentRepository.AddAsync(new FileContent
                    {
                        Path = fileContent.Path,
                        IsActive = fileContent.IsActive,
                        IsDeleted = false
                    }, cancellationToken);
                }
                else
                {
                    // Mevcut kayıt
                    var existingFileContent = await _fileContentRepository.GetByIdAsync(fileContent.Id.Value, cancellationToken);
                    if (existingFileContent != null)
                    {
                        existingFileContent.Path = fileContent.Path;
                        existingFileContent.IsActive = fileContent.IsActive;
                        existingFileContent.IsDeleted = false;
                        _fileContentRepository.Update(existingFileContent);
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Dosya içerikleri başarıyla kaydedildi.";
        }
    }
}
