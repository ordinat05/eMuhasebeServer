using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Utilities.DirectoryPermissionUtility;
using eMuhasebeServer.Domain.Utilities.FileStorageBasePath;
using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;



namespace eMuhasebeServer.Infrastructure.Services
{
    public class WordService : IWordService
    {
        private readonly ILogger<WordService> _logger;

        public WordService(ILogger<WordService> logger)
        {
            _logger = logger;
        }

        public string ConvertWordToHtmlSrv(string sourceFolderPathFileNameFileExtension, string sourceFileNameFileExtension, string targetFolderPath, string guidIdNewFileName)
        {
            // Kaynak olan dosyanın tam adresi
            //string sourceFolderPathFileNameFileExtension = "C:\\ArsivDevelopment\\tammerkezFileStock\\BackendFiles\\ExcelInterop\\excel_uploads\\14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd.docx";
            // Kaynak olan dosyanın , Dosya adı ve Dosya Uzantısı
            //string sourceFileNameFileExtension = "14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd.docx";
            // Kaynak dosyanın Kopyalancağı Dizin. Hedef dosya adresi . 
            //string targetFolderPath = "C:\ArsivDevelopment\tammerkezFileStock\BackendFiles\ExcelInterop\excel_uploads";
            // Kaynak dosyanın Alacağı yeni Dosya Adı.
            //string guidIdNewFileName = "14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd";
            // RETURN staticFilesPath. Proje içindeki StaticFiles klasöründeki, Convert HTM dosya adresi + Dosya Adı + Dosya uzantısı

            try
            {
                // TODO TODO TODO
                // Proje ana dizinine göre tam dosya yolunu oluştur
                //string projectRootPath = Directory.GetCurrentDirectory();
                string projectRootPath = FileStorageBasePaths.BasePath8;
                string htmExportFolderPath = Path.Combine(projectRootPath, targetFolderPath, guidIdNewFileName);

                // Hedef klasörü oluştur
                //Directory.CreateDirectory(htmExportFolderPath);
                CreateDirectoryPermissionUtility.SetFolderPermissions(htmExportFolderPath);

                // HTML olarak kaydedilecek dosya yolunu oluştur
                var outputFilePath = Path.Combine(htmExportFolderPath, guidIdNewFileName + ".htm");

                // Eğer dosya zaten varsa sil
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }

                // Word uygulamasını başlat ve dosyayı aç
                var wordApp = new Microsoft.Office.Interop.Word.Application();
                var wordDoc = wordApp.Documents.Open(sourceFolderPathFileNameFileExtension);

                // Word dosyasını HTML formatında kaydet
                wordDoc.SaveAs2(outputFilePath, WdSaveFormat.wdFormatHTML);

                // Göreceli dosya yolu (targetFolderPath ve sonrasını döndür)
                string staticFilesPath = outputFilePath.Substring(outputFilePath.IndexOf(targetFolderPath));

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

        //public string StaticFilesConvertWordToHtmlSrv3(string sourceFolderPathFileNameFileExtension, string sourceFileNameFileExtension, string targetFolderPath, string guidIdNewFileName)
        //{
        //    // Kaynak olan dosyanın tam adresi
        //    //string sourceFolderPathFileNameFileExtension = "C:\\ArsivDevelopment\\tammerkezFileStock\\BackendFiles\\ExcelInterop\\excel_uploads\\14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd.docx";
        //    // Kaynak olan dosyanın , Dosya adı ve Dosya Uzantısı
        //    //string sourceFileNameFileExtension = "14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd.docx";
        //    // Kaynak dosyanın Kopyalancağı Dizin. Hedef dosya adresi . 
        //    //string targetFolderPath = "C:\ArsivDevelopment\tammerkezFileStock\BackendFiles\ExcelInterop\excel_uploads";
        //    // Kaynak dosyanın Alacağı yeni Dosya Adı.
        //    //string guidIdNewFileName = "14fe8352-2b1c-4a6e-be49-ec03f5dfa4bd";
        //    // RETURN staticFilesPath. Proje içindeki StaticFiles klasöründeki, Convert HTM dosya adresi + Dosya Adı + Dosya uzantısı

        //    try
        //    {
        //        // TODO TODO TODO
        //        // Proje ana dizinine göre tam dosya yolunu oluştur
        //        //string projectRootPath = Directory.GetCurrentDirectory();
        //        string projectRootPath = FileStorageBasePaths.BasePath8;
        //        string htmExportFolderPath = Path.Combine(projectRootPath, targetFolderPath, guidIdNewFileName);

        //        // Hedef klasörü oluştur
        //        //Directory.CreateDirectory(htmExportFolderPath);
        //        CreateDirectoryPermissionUtility.SetFolderPermissions(htmExportFolderPath);

        //        // HTML olarak kaydedilecek dosya yolunu oluştur
        //        var outputFilePath = Path.Combine(htmExportFolderPath, guidIdNewFileName + ".htm");

        //        // Eğer dosya zaten varsa sil
        //        if (File.Exists(outputFilePath))
        //        {
        //            File.Delete(outputFilePath);
        //        }

        //        // Word uygulamasını başlat ve dosyayı aç
        //        var wordApp = new Microsoft.Office.Interop.Word.Application();
        //        var wordDoc = wordApp.Documents.Open(sourceFolderPathFileNameFileExtension);

        //        // Word dosyasını HTML formatında kaydet
        //        wordDoc.SaveAs2(outputFilePath, WdSaveFormat.wdFormatHTML);

        //        // Göreceli dosya yolu (targetFolderPath ve sonrasını döndür)
        //        string staticFilesPath = outputFilePath.Substring(outputFilePath.IndexOf(targetFolderPath));

        //        // Word dosyasını kapat ve kaynakları serbest bırak
        //        object missing = Type.Missing;
        //        wordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
        //        wordApp.Quit();
        //        ReleaseObject(wordDoc);
        //        ReleaseObject(wordApp);

        //        // HTML dosyasının kaydedildiği göreceli yolu döndür
        //        return staticFilesPath;
        //    }
        //    catch (Exception)
        //    {
        //        return "Word dosya işlemi başarısız oldu.";
        //    }
        //}

        public string StaticFilesConvertWordToHtmlSrv3(string sourceFolderPathFileNameFileExtension, string sourceFileNameFileExtension, string targetFolderPath, string guidIdNewFileName)
        {
            try
            {
                _logger.LogInformation("1. İşlem başlıyor...");

                string projectRootPath = FileStorageBasePaths.BasePath8;
                if (string.IsNullOrEmpty(projectRootPath))
                {
                    _logger.LogError("Project root path 'BasePath8' null veya boş.");
                    return "Project root path 'BasePath8' null veya boş.";
                }

                string htmExportFolderPath = Path.Combine(projectRootPath, targetFolderPath, guidIdNewFileName);
                CreateDirectoryPermissionUtility.SetFolderPermissions(htmExportFolderPath);

                var outputFilePath = Path.Combine(htmExportFolderPath, guidIdNewFileName + ".htm");
                if (string.IsNullOrEmpty(outputFilePath))
                {
                    _logger.LogError("Output file path 'outputFilePath' null veya boş.");
                    return "Output file path 'outputFilePath' null veya boş.";
                }

                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }

                _logger.LogInformation("Word uygulaması başlatılıyor...");
                var wordApp = new Microsoft.Office.Interop.Word.Application();
                if (wordApp == null)
                {
                    _logger.LogError("Word uygulaması başlatılamadı.");
                    return "Word uygulaması başlatılamadı.";
                }

                try
                {
                    _logger.LogInformation("Dosya açılıyor: {SourcePath}", sourceFolderPathFileNameFileExtension);

                    if (!File.Exists(sourceFolderPathFileNameFileExtension))
                    {
                        _logger.LogError("Kaynak dosya mevcut değil: {SourcePath}", sourceFolderPathFileNameFileExtension);
                        return $"Kaynak dosya mevcut değil: {sourceFolderPathFileNameFileExtension}";
                    }

                    var wordDoc = wordApp.Documents.Open(sourceFolderPathFileNameFileExtension);
                    if (wordDoc == null)
                    {
                        _logger.LogError("Word belgesi açılmadı.");
                        return "Word belgesi açılmadı.";
                    }

                    _logger.LogInformation("HTML olarak kaydediliyor: {OutputPath}", outputFilePath);
                    try
                    {
                        wordDoc.SaveAs2(outputFilePath, WdSaveFormat.wdFormatHTML);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "HTML olarak kaydetme sırasında hata oluştu.");
                        return $"HTML olarak kaydetme sırasında hata: {ex.Message}";
                    }

                    _logger.LogInformation("HTML olarak başarıyla kaydedildi");

                    wordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
                    wordApp.Quit();

                    ReleaseObject(wordDoc);
                    ReleaseObject(wordApp);

                    string staticFilesPath = outputFilePath.Substring(outputFilePath.IndexOf(targetFolderPath));
                    return staticFilesPath;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Word işlemleri sırasında hata oluştu.");
                    wordApp.Quit();
                    ReleaseObject(wordApp);
                    return $"Word işlemleri sırasında hata: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Word dosya işlemi başarısız oldu.");
                return $"Word dosya işlemi başarısız oldu: {ex.Message}";
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


