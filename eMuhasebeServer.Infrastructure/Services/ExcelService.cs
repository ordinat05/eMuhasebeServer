using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eMuhasebeServer.Application.InterfaceService;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using eMuhasebeServer.Domain.Utilities.DirectoryPermissionUtility;
using eMuhasebeServer.Domain.Utilities.FileStorageBasePath;


namespace eMuhasebeServer.Infrastructure.Services;

public class ExcelService : IExcelService
{
    public string ConvertExcelToHtmlSrv(string FolderPathAndFileName, string FileNameAndFileExtension, string UploadPath, string FileGuidIdName)
    {
        try
        {
            // İSTEK 1 : Burada HTM dosyalarını içine koymak için klasör oluşturacağız. UploadPath+ fileGuidIdName kullanılacak.
            // İSTEK 2 : Oluşturulan dizin adı değişkeni htmOutputDirectory olacak.
            // İSTEK 3 : htmOutputDirectory return edilecek.
            // İSTEK 4 : İşlemler senkron olacak. HTM dönüştürme işlemi bittiğinde, cevap dönecek.

            string htmOutputDirectory = Path.Combine(UploadPath, FileGuidIdName);
            //Directory.CreateDirectory(htmOutputDirectory);
            CreateDirectoryPermissionUtility.SetFolderPermissions(htmOutputDirectory);
            

            // Excel uygulamasını aç
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkBook = xlApp.Workbooks.Open(FolderPathAndFileName);
            var sheet = (Worksheet)xlWorkBook.Worksheets.Item[1];

            int rowIndex = 2;
            int columnIndex = 2;
            var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowIndex, columnIndex];
            cell.Value2 = "Merhaba";

            // HTML dosyası olarak kaydedilecek dosyaNIN yolu
            var outputFilePath = Path.Combine(htmOutputDirectory, Path.GetFileNameWithoutExtension(FolderPathAndFileName) + ".htm");

            // Excel dosyasını HTML formatında kaydet
            xlWorkBook.SaveAs(outputFilePath, XlFileFormat.xlHtml);
            // Dikkat HTM dosyasının kaydedileceği dizin

            // Excel dosyasını kapat
            object missing = Type.Missing;
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            // Kullanılan kaynakları serbest bırak

            xlWorkBook.Close(saveChanges, missing, missing);
            xlApp.Quit();
            ReleaseObject(sheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

            return htmOutputDirectory;
        }
        catch (Exception)
        {
            return "Excel Dosya işlemi başarısız oldu.";
        }
    }

    //Başarılı .
    //HATA1 : Sonuna Dosya adını tablodan çektiğini koyuyor. GuidId ile Yeni isim verilerek HTM dosyası oluşturması gerekiyor. Eski isimi kullanmayacak.
    //HATA2 : .htm Dosya yolunu verirken yanlış veriyor, Path i c: sürücüsünden başlatıyor.
    //HATA 3 : Kaydededileceği Dizini string kendisi yazarak girmek lazım. 
    public string StaticFilesConvertExcelToHtmlSrv2(string FolderPathAndFileName, string FileNameAndFileExtension, string UploadPath, string FileGuidIdName)
    {
        try
        {
            // İSTEK 1 : "StaticFiles" dizinine kaydetmek için klasör oluştur.
            //string staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "aaaStaticFiles/f1menu7", FileGuidIdName);
            string staticFilesPath = Path.Combine(FileStorageBasePaths.BasePath8 , FileGuidIdName);
            //Directory.CreateDirectory(staticFilesPath);
            CreateDirectoryPermissionUtility.SetFolderPermissions(staticFilesPath);

            // Excel uygulamasını aç
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkBook = xlApp.Workbooks.Open(FolderPathAndFileName);
            var sheet = (Worksheet)xlWorkBook.Worksheets.Item[1];

            // HTML olarak kaydedilecek dosya yolunu oluştur
            var outputFilePath = Path.Combine(staticFilesPath, Path.GetFileNameWithoutExtension(FolderPathAndFileName) + ".htm");
            // Eğer dizinde dosya varsa sil
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            // Excel dosyasını HTML formatında kaydet
            xlWorkBook.SaveAs(outputFilePath, XlFileFormat.xlHtml);

            // Excel dosyasını kapat ve kaynakları serbest bırak
            object missing = Type.Missing;
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            xlWorkBook.Close(saveChanges, missing, missing);
            xlApp.Quit();
            ReleaseObject(sheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

            // HTML dosyasının kaydedildiği yolu döndür
            return staticFilesPath;
        }
        catch (Exception)
        {
            return "Excel dosya işlemi başarısız oldu.";
        }
    }
   
    public string StaticFilesConvertExcelToHtmlSrv3(string FolderPathAndFileName, string FileNameAndFileExtension, string UploadPath, string FileGuidIdName)
    {
        try
        {
            // Proje ana dizinine göre tam dosya yolunu oluştur
            //string projectRootPath = Directory.GetCurrentDirectory();
            string projectRootPath = FileStorageBasePaths.BasePath8;
            string fullUploadPath = Path.Combine(projectRootPath, UploadPath, FileGuidIdName);

            // İlgili klasörü oluştur (aaaStaticFiles\\f1menu7\\FileGuidIdName şeklinde)
            // Directory.CreateDirectory(fullUploadPath);
            CreateDirectoryPermissionUtility.SetFolderPermissions(fullUploadPath);

            // Dosya adı ve uzantısını ayarla
            string originalFileExtension = Path.GetExtension(FileNameAndFileExtension);
            string newGuidFileName = FileGuidIdName + originalFileExtension;

            // HTML olarak kaydedilecek dosya yolunu oluştur
            var outputFilePath = Path.Combine(fullUploadPath, Path.GetFileNameWithoutExtension(FileGuidIdName) + ".htm");

            // Eğer dosya zaten varsa sil
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            // Excel uygulamasını aç
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkBook = xlApp.Workbooks.Open(FolderPathAndFileName);
            var sheet = (Worksheet)xlWorkBook.Worksheets.Item[1];

            // Excel dosyasını HTML formatında kaydet
            xlWorkBook.SaveAs(outputFilePath, XlFileFormat.xlHtml);

            string staticFilesPath = outputFilePath.Substring(outputFilePath.IndexOf(UploadPath));
           

            // Excel dosyasını kapat ve kaynakları serbest bırak
            object missing = Type.Missing;
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            xlWorkBook.Close(saveChanges, missing, missing);
            xlApp.Quit();
            ReleaseObject(sheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

            // HTML dosyasının kaydedildiği yolu döndür
            return staticFilesPath;
        }
        catch (Exception)
        {
            return "Excel dosya işlemi başarısız oldu.";
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
