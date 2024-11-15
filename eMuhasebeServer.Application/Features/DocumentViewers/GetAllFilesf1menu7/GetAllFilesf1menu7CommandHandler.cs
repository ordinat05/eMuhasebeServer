using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DocumentViewers.GetAllFilesf1menu7
{
    internal sealed class GetAllFilesf1menu7CommandHandler :
           IRequestHandler<GetAllFilesf1menu7Command, Result<List<DocumentViewerf1menu7>>>
    {
        private readonly IDocumentViewerRepository _repository;

        public GetAllFilesf1menu7CommandHandler(IDocumentViewerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<DocumentViewerf1menu7>>> Handle(
            GetAllFilesf1menu7Command request,
            CancellationToken cancellationToken)
        {
            try
            {
                var records = await _repository.GetAllAsync(cancellationToken);
                return Result<List<DocumentViewerf1menu7>>.Succeed(records);
            }
            catch (Exception ex)
            {
                return Result<List<DocumentViewerf1menu7>>.Failure($"Kayıtlar getirilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
