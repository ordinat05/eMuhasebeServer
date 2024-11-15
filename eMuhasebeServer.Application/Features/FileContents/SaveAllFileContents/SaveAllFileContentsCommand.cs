using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContents.SaveAllFileContents
{
    public sealed record SaveAllFileContentsCommand(List<FileContentDto> FileContents) : IRequest<Result<string>>;

    public class FileContentDto
    {
        public Guid? Id { get; set; }
        public string? Path { get; set; }
        public bool IsActive { get; set; }
    }
}