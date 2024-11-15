using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DropZones.DropZonePreviewDwg;

public sealed record DropZonePreviewDwgCommand(IFormFile File) : IRequest<Result<string>>;
