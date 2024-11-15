using eMuhasebeServer.Domain.Entities.Dtos;
using MediatR;

namespace eMuhasebeServer.Application.Features.DocumentViewers.UpdateByIdf1menu7;

public sealed record UpdateByIdf1menu7Command : IRequest<Response<UpdateByIdf1menu7Dto>>
{
    public Guid Id { get; init; }
    public string? Konu { get; init; }
    public string? Not { get; init; }
    public DateTime? SaveDateTime { get; init; }
    public string? Filename { get; init; }
    public string? Filesize { get; init; }
    public string? TokenLoaderId { get; init; }
    public string? UserOtherPcLoginSessionId { get; init; }
    public string? Color { get; init; }
    public bool IsActive { get; init; }
    public int SortIndex { get; init; }
}

public class UpdateByIdf1menu7Dto
{
    public Guid Id { get; set; }
    public string? Konu { get; set; }
    public string? Not { get; set; }
    public DateTime? SaveDateTime { get; set; }
    public string? Filename { get; set; }
    public string? Filesize { get; set; }
    public string? TokenLoaderId { get; set; }
    public string? UserOtherPcLoginSessionId { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; }
    public int SortIndex { get; set; }
}