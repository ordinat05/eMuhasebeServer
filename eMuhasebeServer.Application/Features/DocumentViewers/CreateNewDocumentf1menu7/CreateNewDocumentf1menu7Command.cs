using eMuhasebeServer.Domain.Entities.Dtos;
using MediatR;

namespace eMuhasebeServer.Application.Features.DocumentViewers.CreateNewDocumentf1menu7
{
    public sealed record CreateNewDocumentf1menu7Command : IRequest<Response<CreateNewDocumentf1menu7Dto>>
    {
        public string? Konu { get; init; }
        public string? Not { get; init; }
        public DateTime? SaveDateTime { get; init; }
        public Guid Id { get; init; }
        public string? Filename { get; init; }
        public string? Filesize { get; init; }
        public string? TokenLoaderId { get; init; }
        public string? UserOtherPcLoginSessionId { get; init; }
        public bool IsActive { get; init; }
        public bool IsDeleted { get; init; }
        public int SortIndex { get; init; }
    }

    public class CreateNewDocumentf1menu7Dto
    {
        public Guid Id { get; set; }
        public string? Konu { get; set; }
        public string? Not { get; set; }
        public DateTime? SaveDateTime { get; set; }
        public string? Filename { get; set; }
        public string? Filesize { get; set; }
        public string? TokenLoaderId { get; set; }
        public string? UserOtherPcLoginSessionId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int SortIndex { get; set; }
    }
}
