using eMuhasebeServer.Domain.Entities.Dtos;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;

namespace eMuhasebeServer.Application.Features.DocumentViewers.DeleteByIdDocumentViewerf1menu7;


public sealed class DeleteByIdDocumentViewerf1menu7CommandHandler
    : IRequestHandler<DeleteByIdDocumentViewerf1menu7Command, Response<bool>>
{
    private readonly IDocumentViewerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteByIdDocumentViewerf1menu7CommandHandler(
        IDocumentViewerRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> Handle(
        DeleteByIdDocumentViewerf1menu7Command request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _repository.DeleteByIdAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Response<bool>.Success(true, 200);
        }
        catch (Exception ex)
        {
            return Response<bool>.Fail($"Kayıt silinirken bir hata oluştu: {ex.Message}", 500, true);
        }
    }
}
