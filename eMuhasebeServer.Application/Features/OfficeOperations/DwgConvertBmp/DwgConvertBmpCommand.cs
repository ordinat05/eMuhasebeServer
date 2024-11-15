using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace eMuhasebeServer.Application.Features.OfficeOperations.DwgConvertBmp;


public sealed record DwgConvertBmpCommand(IFormFile File) : IRequest<Result<string>>;

//C: \Users\Lenovo\source\repos\ProinsSolutions\eMuhasebeServer\eMuhasebeServer.Application\Features\DropZones\DropZonePreviewDwg\DropZonePreviewDwgCommand.cs Burada zaten var. Bu yedek