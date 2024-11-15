using eMuhasebeServer.Application.Features.DocumentViewers.SaveDropZoneOneFiles;
using eMuhasebeServer.Domain.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace eMuhasebeServer.Application.Features.DocumentViewers.SaveAllDropZoneFiles;


public class SaveAllDropZoneFileCommand : IRequest<Response<SaveAllDropZoneFileDto>>
{
    public IFormFile? File { get; set; }
    public string? FileGuidId { get; set; }
    public string? TokenLoaderId { get; set; }
}

public class SaveAllDropZoneFileDto
{
    public string? Name { get; set; }
    public string? Filename { get; set; }
    public string? Filesize { get; set; }
    public string? GuidId { get; set; }
    public string? TokenLoaderId { get; set; }
    public string? FilePath { get; set; }
}