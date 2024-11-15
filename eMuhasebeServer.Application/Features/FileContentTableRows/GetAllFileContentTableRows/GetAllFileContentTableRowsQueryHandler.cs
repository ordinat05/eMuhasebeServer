using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContentTableRows.GetAllFileContentTableRows
{
    internal sealed class GetAllFileContentTableRowsQueryHandler : IRequestHandler<GetAllFileContentTableRowsQuery, Result<List<FileContentTableRow>>>
    {
        private readonly IFileContentTableRowRepository _repository;

        public GetAllFileContentTableRowsQueryHandler(IFileContentTableRowRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<FileContentTableRow>>> Handle(GetAllFileContentTableRowsQuery request, CancellationToken cancellationToken)
        {
            var fileContentTableRows = await _repository.GetAllAsync(cancellationToken);
            return fileContentTableRows.Where(fctr => !fctr.IsDeleted).ToList();
        }
    }
}