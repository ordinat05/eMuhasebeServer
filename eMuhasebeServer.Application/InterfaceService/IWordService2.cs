using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Application.InterfaceService
{
    public interface IWordService2
    {
        string ConvertWordToHtmlSrv2(string FolderPathAndFileName, string FileName, string UploadPath, string FileGuidIdName);
    }
}
