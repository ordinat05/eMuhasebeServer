using System;

namespace eMuhasebeServer.Application.InterfaceService
{
    public interface IWordService
    {
        string ConvertWordToHtmlSrv(string FolderPathAndFileName, string FileName, string UploadPath, string FileGuidIdName);
        string StaticFilesConvertWordToHtmlSrv3(string FolderPathAndFileName, string FileName, string UploadPath, string FileGuidIdName);

    }
}
