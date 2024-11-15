using eMuhasebeServer.Application.InterfaceService;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Infrastructure.Services
{
    internal class WordService2 : IWordService2
    {
        public string ConvertWordToHtmlSrv2(string FolderPathAndFileName, string FileNameAndFileExtension, string UploadPath, string FileGuidIdName)
        {
            // Kaynak olan dosyanın tam adresi
            //string FolderPathAndFileName = "C:\\ArsivDevelopment\\tammerkezFileStock\\BackendFiles\\ExcelInterop\\excel_uploads\\14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd.docx";
            // Kaynak olan dosyanın , Dosya adı ve Dosya Uzantısı
            //string FileNameAndFileExtension = "14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd.docx";
            // Kaynak dosyanın Kopyalancağı Dizin. Hedef dosya adresi . 
            //string UploadPath = "C:\ArsivDevelopment\tammerkezFileStock\BackendFiles\ExcelInterop\excel_uploads";
            // Kaynak dosyanın Alacağı yeni Dosya Adı.
            //string FileGuidIdName = "14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd";
            // RETURN staticFilesPath. Proje içindeki StaticFiles klasöründeki, Convert HTM dosya adresi + Dosya Adı + Dosya uzantısı

            try
            {
                // Proje ana dizinine göre tam dosya yolunu oluştur
                string projectRootPath = Directory.GetCurrentDirectory();
                string fullUploadPath = Path.Combine(projectRootPath, UploadPath, FileGuidIdName);

                // Hedef klasörü oluştur
                Directory.CreateDirectory(fullUploadPath);

                // HTML olarak kaydedilecek dosya yolunu oluştur
                var outputFilePath = Path.Combine(fullUploadPath, FileGuidIdName + ".htm");

                // Eğer dosya zaten varsa sil
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }

                // Word uygulamasını başlat ve dosyayı aç
                var wordApp = new Microsoft.Office.Interop.Word.Application();
                var wordDoc = wordApp.Documents.Open(FolderPathAndFileName);

                // Word dosyasını HTML formatında kaydet
                wordDoc.SaveAs2(outputFilePath, WdSaveFormat.wdFormatHTML);

                // Göreceli dosya yolu (UploadPath ve sonrasını döndür)
                string staticFilesPath = outputFilePath.Substring(outputFilePath.IndexOf(UploadPath));

                // Word dosyasını kapat ve kaynakları serbest bırak
                object missing = Type.Missing;
                wordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
                wordApp.Quit();
                ReleaseObject(wordDoc);
                ReleaseObject(wordApp);

                // HTML dosyasının kaydedildiği göreceli yolu döndür
                return staticFilesPath;
            }
            catch (Exception)
            {
                return "Word dosya işlemi başarısız oldu.";
            }
        }

      

        private void ReleaseObject(object? obj)
        {
            try
            {
#if WINDOWS
                if (obj != null && System.Runtime.InteropServices.Marshal.IsComObject(obj))
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                }
#endif
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
