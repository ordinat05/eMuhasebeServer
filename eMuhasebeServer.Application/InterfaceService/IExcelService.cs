using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Application.InterfaceService
{
    public interface IExcelService
    {
        string ConvertExcelToHtmlSrv(string FolderPathAndFileName, string FileName, string UploadPath, string FileGuidIdName);   
        string StaticFilesConvertExcelToHtmlSrv2(string FolderPathAndFileName, string FileName, string UploadPath, string FileGuidIdName);
        string StaticFilesConvertExcelToHtmlSrv3(string FolderPathAndFileName, string FileName, string UploadPath, string FileGuidIdName);

    }
}
