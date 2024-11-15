using eMuhasebeServer.Application.Features.DropZones.DropZonePreviewDwg;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace eMuhasebeServer.Application.Features.OfficeOperations.Excel.ExcelConvertHtm;

public sealed record ExcelConvertHtmCommand(string FolderPathAndFileName, string FileNameAndFileExtension, string UploadPath, string FileGuidIdName) : IRequest<Result<string>>;

