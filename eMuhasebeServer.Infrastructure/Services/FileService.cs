//using eMuhasebeServer.Application.InterfaceService;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;

//namespace eMuhasebeServer.Infrastructure.Services
//{
//    public class FileService : IFileService
//    {
//        private readonly IHostEnvironment _environment;
//        private readonly IConfiguration _configuration;

//        public FileService(IHostEnvironment environment, IConfiguration configuration)
//        {
//            _environment = environment;
//            _configuration = configuration;
//        }

//        public string GetBasePath()
//        {
//            return _configuration.GetValue<string>("FileStorage:BasePath") ?? "Varsayilan/Dosya/Yolu/Bulunamadı";
//        }
//        public string GetBasePath2()
//        {
//            return _configuration.GetValue<string>("FileStorage:BasePath2") ?? "Default/File/Path/NotFound";
//        }
//        public string GetBasePath3()
//        {
//            return _configuration.GetValue<string>("FileStorage:BasePath3") ?? "Default/File/Path/NotFound";
//        }      
//        public string GetBasePath6()
//        {
//            return _configuration.GetValue<string>("FileStorage:BasePath6") ?? "Default/File/Path/NotFound";
//        }

//        public string GetBasePath7()
//        {
//            return _configuration.GetValue<string>("FileStorage:BasePath7") ?? "Default/File/Path/NotFound";
//        }



//        public async Task<string> UploadFileSrvAsync(IFormFile file, string folderName, CancellationToken cancellationToken)
//        {
//            if (file == null || file.Length == 0)
//                throw new ArgumentException("File is empty", nameof(file));

//            var basePath = GetBasePath();
//            var uploadPath = Path.Combine(basePath, folderName);
//            if (!Directory.Exists(uploadPath))
//                Directory.CreateDirectory(uploadPath);

//            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
//            var filePath = Path.Combine(uploadPath, fileName);

//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await file.CopyToAsync(stream, cancellationToken);
//            }

//            return filePath;
//        }

//        public async Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadFileExcelSrvAsync(IFormFile file, string pathPlus, CancellationToken cancellationToken)
//        {
//            if (file == null || file.Length == 0)
//                throw new ArgumentException("File is empty", nameof(file));

//            var basePath = GetBasePath3();
//            var uploadPath = Path.Combine(basePath, pathPlus);
//            if (!Directory.Exists(uploadPath))
//                Directory.CreateDirectory(uploadPath);

//            // Önce Guid'i oluşturuyoruz ve hem fileName hem de fileGuidIdName için kullanıyoruz
//            var fileGuidIdName = Guid.NewGuid().ToString();
//            var fileNameAndFileExtension = $"{fileGuidIdName}{Path.GetExtension(file.FileName)}"; // Dosya ismi oluşturuluyor
//            var folderPathAndFileName = Path.Combine(uploadPath, fileNameAndFileExtension);

//            using (var stream = new FileStream(folderPathAndFileName, FileMode.Create))
//            {
//                await file.CopyToAsync(stream, cancellationToken);
//                //b35924fb-5eb4-46f5-be07-531a98491d43.xlsx yeni Id li Excel Dosyası oluşturuldu
//            }

//            return (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName);

//        }   

//        public async Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadFileWordSrvAsync(IFormFile file, string pathPlus, CancellationToken cancellationToken)
//        {
//            if (file == null || file.Length == 0)
//                throw new ArgumentException("File is empty", nameof(file));

//            var basePath = GetBasePath3();
//            var uploadPath = Path.Combine(basePath, pathPlus);
//            if (!Directory.Exists(uploadPath))
//                Directory.CreateDirectory(uploadPath);

//            // Önce Guid'i oluşturuyoruz ve hem fileName hem de fileGuidIdName için kullanıyoruz
//            var fileGuidIdName = Guid.NewGuid().ToString();
//            var fileNameAndFileExtension = $"{fileGuidIdName}{Path.GetExtension(file.FileName)}"; // Dosya ismi oluşturuluyor
//            var folderPathAndFileName = Path.Combine(uploadPath, fileNameAndFileExtension);

//            using (var stream = new FileStream(folderPathAndFileName, FileMode.Create))
//            {
//                await file.CopyToAsync(stream, cancellationToken);
//                //b35924fb-5eb4-46f5-be07-531a98491d43.xlsx yeni Id li Excel Dosyası oluşturuldu
//            }

//            return (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName);

//        }

//        public async Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadDocumentViewerFileAsync(IFormFile file, CancellationToken cancellationToken)
//        {
//            if (file == null || file.Length == 0)
//                throw new ArgumentException("File is empty", nameof(file));

//            var basePath = GetBasePath6();
//            var uploadPath = basePath;
//            if (!Directory.Exists(uploadPath))
//                Directory.CreateDirectory(uploadPath);

//            var fileGuidIdName = Guid.NewGuid().ToString();
//            var fileNameAndFileExtension = $"{fileGuidIdName}{Path.GetExtension(file.FileName)}";
//            var folderPathAndFileName = Path.Combine(uploadPath, fileNameAndFileExtension);

//            using (var stream = new FileStream(folderPathAndFileName, FileMode.Create))
//            {
//                await file.CopyToAsync(stream, cancellationToken);
//            }

//            return (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName);
//        }
//        // Dosya Yükleme Silme Çalışıyor Versiyon 1 START
//        #region Dosya Yükleme Silme Çalışıyor

//        public async Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> DropZoneOneFileDetect(IFormFile file, string fileGuidId, CancellationToken cancellationToken)
//        {
//            if (file == null || file.Length == 0)
//                throw new ArgumentException("File is empty", nameof(file));

//            var basePath = GetBasePath6();
//            var uploadPath = basePath;
//            if (!Directory.Exists(uploadPath))
//                Directory.CreateDirectory(uploadPath);

//            var fileNameAndFileExtension = $"{fileGuidId}{Path.GetExtension(file.FileName)}";
//            var folderPathAndFileName = Path.Combine(uploadPath, fileNameAndFileExtension);

//            using (var stream = new FileStream(folderPathAndFileName, FileMode.Create))
//            {
//                await file.CopyToAsync(stream, cancellationToken);
//            }

//            return (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidId);
//        }
//        #endregion
//        // Dosya Yükleme Silme Çalışıyor Versiyon 1 END
//        #region GetBasePath6 ya kaydedilmiş olan dosyayı  GetBasePath7 ye taşımak



//        public async Task MoveToBasePath7Async(string fileGuidId, CancellationToken cancellationToken)
//        {
//            var basePath6 = GetBasePath6();
//            var basePath7 = GetBasePath7();

//            // "*" kullanarak uzantıdan bağımsız olarak dosyayı bul
//            var sourceFile = Directory.GetFiles(basePath6, $"{fileGuidId}.*").FirstOrDefault();

//            if (sourceFile != null)
//            {
//                var fileName = Path.GetFileName(sourceFile);
//                var destinationPath = Path.Combine(basePath7, fileName);
//                // Hedef klasör yoksa oluştur
//                Directory.CreateDirectory(basePath7);

//                try
//                {
//                    // Önce dosyayı kopyala
//                    await Task.Run(() => File.Copy(sourceFile, destinationPath, true), cancellationToken);

//                    // Kopyalama başarılı olduktan sonra kaynak dosyayı sil
//                    await Task.Run(() => File.Delete(sourceFile), cancellationToken);
//                }
//                catch (Exception ex)
//                {
//                    // Hata durumunda loglama yapabilir veya exception fırlatabilirsiniz
//                    throw new Exception($"Dosya işlemi sırasında hata oluştu: {ex.Message}", ex);
//                }
//            }
//        }

//        #endregion

//    }
//}

using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Utilities.FileStorageBasePath;
using Microsoft.AspNetCore.Http;

namespace eMuhasebeServer.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFileSrvAsync(IFormFile file, string folderName, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty", nameof(file));

            var basePath = FileStorageBasePaths.BasePath;
            var uploadPath = Path.Combine(basePath, folderName);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return filePath;
        }

        public async Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadFileExcelSrvAsync(IFormFile file, string pathPlus, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty", nameof(file));

            var basePath = FileStorageBasePaths.BasePath3;
            var uploadPath = Path.Combine(basePath, pathPlus);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileGuidIdName = Guid.NewGuid().ToString();
            var fileNameAndFileExtension = $"{fileGuidIdName}{Path.GetExtension(file.FileName)}";
            var folderPathAndFileName = Path.Combine(uploadPath, fileNameAndFileExtension);

            using (var stream = new FileStream(folderPathAndFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName);
        }

        public async Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadFileWordSrvAsync(IFormFile file, string pathPlus, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty", nameof(file));

            var basePath = FileStorageBasePaths.BasePath3;
            var uploadPath = Path.Combine(basePath, pathPlus);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileGuidIdName = Guid.NewGuid().ToString();
            var fileNameAndFileExtension = $"{fileGuidIdName}{Path.GetExtension(file.FileName)}";
            var folderPathAndFileName = Path.Combine(uploadPath, fileNameAndFileExtension);

            using (var stream = new FileStream(folderPathAndFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName);
        }

        public async Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> UploadDocumentViewerFileAsync(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty", nameof(file));

            var basePath = FileStorageBasePaths.BasePath6;
            var uploadPath = basePath;
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileGuidIdName = Guid.NewGuid().ToString();
            var fileNameAndFileExtension = $"{fileGuidIdName}{Path.GetExtension(file.FileName)}";
            var folderPathAndFileName = Path.Combine(uploadPath, fileNameAndFileExtension);

            using (var stream = new FileStream(folderPathAndFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName);
        }

        public async Task<(string folderPathAndFileName, string fileNameAndFileExtension, string uploadPath, string fileGuidIdName)> DropZoneOneFileDetect(IFormFile file, string fileGuidId, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty", nameof(file));

            var basePath = FileStorageBasePaths.BasePath6;
            var uploadPath = basePath;
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileNameAndFileExtension = $"{fileGuidId}{Path.GetExtension(file.FileName)}";
            var folderPathAndFileName = Path.Combine(uploadPath, fileNameAndFileExtension);

            using (var stream = new FileStream(folderPathAndFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidId);
        }

        public async Task MoveToBasePath7Async(string fileGuidId, CancellationToken cancellationToken)
        {
            var basePath6 = FileStorageBasePaths.BasePath6;
            var basePath7 = FileStorageBasePaths.BasePath7;

            var sourceFile = Directory.GetFiles(basePath6, $"{fileGuidId}.*").FirstOrDefault();

            if (sourceFile != null)
            {
                var fileName = Path.GetFileName(sourceFile);
                var destinationPath = Path.Combine(basePath7, fileName);
                Directory.CreateDirectory(basePath7);

                try
                {
                    await Task.Run(() => File.Copy(sourceFile, destinationPath, true), cancellationToken);
                    await Task.Run(() => File.Delete(sourceFile), cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Dosya işlemi sırasında hata oluştu: {ex.Message}", ex);
                }
            }
        }
    }
}