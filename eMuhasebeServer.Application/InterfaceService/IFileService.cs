//using Microsoft.AspNetCore.Http;

//namespace eMuhasebeServer.Application.InterfaceService
//{
//    public interface IFileService
//    {
//        Task<string> UploadFileSrvAsync(IFormFile file, string folderName, CancellationToken cancellationToken);
//        Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadFileExcelSrvAsync(IFormFile file, string folderName, CancellationToken cancellationToken);
//        Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadFileWordSrvAsync(IFormFile file, string folderName, CancellationToken cancellationToken);

//        string GetBasePath();
//        string GetBasePath3();
//        string GetBasePath6();
//        Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadDocumentViewerFileAsync(IFormFile file, CancellationToken cancellationToken); 
//        Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> DropZoneOneFileDetect(IFormFile file, string fileGuidId, CancellationToken cancellationToken);
//        string GetBasePath7();
//        Task MoveToBasePath7Async(string fileGuidId, CancellationToken cancellationToken);
//    }
//}

using Microsoft.AspNetCore.Http;

public interface IFileService
{
    Task<string> UploadFileSrvAsync(IFormFile file, string folderName, CancellationToken cancellationToken);
    Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadFileExcelSrvAsync(IFormFile file, string folderName, CancellationToken cancellationToken);
    Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadFileWordSrvAsync(IFormFile file, string folderName, CancellationToken cancellationToken);
    Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadDocumentViewerFileAsync(IFormFile file, CancellationToken cancellationToken);
    Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> DropZoneOneFileDetect(IFormFile file, string fileGuidId, CancellationToken cancellationToken);
    Task MoveToBasePath7Async(string fileGuidId, CancellationToken cancellationToken);
}