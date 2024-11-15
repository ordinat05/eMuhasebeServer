using eMuhasebeServer.Domain.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace eMuhasebeServer.Application.Features.DocumentViewers.SaveDropZoneOneFiles;

public class SaveDropZoneOneFileCommand : IRequest<Response<SaveDropZoneOneFileDto>>
{
    public IFormFile? File { get; set; }
    public string? FileGuidId { get; set; }
    public string? UserOtherPcLoginSessionId { get; set; }
    public string? TokenLoaderId { get; set; }
}

public class SaveDropZoneOneFileDto
{
    public string? Filename { get; set; }
    public string? Filesize { get; set; }
    public string? FileGuidId { get; set; }
    public string? UserOtherPcLoginSessionId { get; set; }
    public string? TokenLoaderId { get; set; }
    public string? FilePath { get; set; }
}
