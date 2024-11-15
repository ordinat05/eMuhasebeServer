using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FileContent2s.SaveAllFileContent2s
{
    public sealed record SaveAllFileContent2sCommand(List<FileContent2Dto> FileContents) : IRequest<Result<string>>;

    public class FileContent2Dto
    {
        public Guid? Id { get; set; }
        public string? Path { get; set; }
        public bool IsActive { get; set; }
        public int SortIndex { get; set; }
        public Guid FileContentTableRowId { get; set; }
    }
}