using eMuhasebeServer.Application.Features.OfficeOperations.Excel.ExcelConvertHtm;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace eMuhasebeServer.Application.Features.OfficeOperations.Word.WordConvertHtm;

public sealed record WordConvertHtmCommand(string FolderPathAndFileName, string FileNameAndFileExtension, string UploadPath, string FileGuidIdName) : IRequest<Result<string>>;


