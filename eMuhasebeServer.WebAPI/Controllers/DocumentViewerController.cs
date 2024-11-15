using eMuhasebeServer.Application.Features.DocumentViewers.CreateNewDocumentf1menu7;
using eMuhasebeServer.Application.Features.DocumentViewers.GetAllFilesf1menu7;
using eMuhasebeServer.Domain.Entities.Dtos.DocumentViewerDto;
using eMuhasebeServer.Application.Features.DocumentViewers.SaveAllDropZoneFiles;
using eMuhasebeServer.Application.Features.DocumentViewers.SaveDropZoneOneFiles;
using eMuhasebeServer.Application.Features.DocumentViewers.UpdateByIdf1menu7;
using eMuhasebeServer.Application.Features.DocumentViewers.UpdateSortIndexf1menu7;
using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Entities.Dtos;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Text.Json;

namespace eMuhasebeServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentViewerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private static readonly ConcurrentDictionary<string, FileUploadInfo> ActiveUploads = new ConcurrentDictionary<string, FileUploadInfo>(); private readonly IConfiguration _configuration;
        private readonly IDocumentViewerRepository _repository;
        private readonly IDocumentViewerPreviewService _documentViewerPreviewService;

        public DocumentViewerController(IMediator mediator, IConfiguration configuration, IDocumentViewerRepository repository, IDocumentViewerPreviewService documentViewerPreviewService)
        {
            _mediator = mediator;
            _configuration = configuration;
            _repository = repository;
            _documentViewerPreviewService = documentViewerPreviewService;
        }

        [RequestSizeLimit(524288000)] // 500 MB Dosya Yükleme Limiti
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)] // 500 MB Dosya Yükleme Limiti
        [HttpPost("SaveAllDropZoneFile")]
        public async Task<IActionResult> SaveAllDropZoneFile(IFormFile file)
        {
            var command = new SaveAllDropZoneFileCommand { File = file };
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // Dosya Yükleme Silme Çalışıyor Versiyon 1 START
        #region Dosya Yükle, Yüklemesi Devam Eden İşlemi İptal Et, Yüklenmiş Dosyayı Sil.


        //[RequestSizeLimit(524288000)] // 500 MB Dosya Yükleme Limiti
        //[RequestFormLimits(MultipartBodyLengthLimit = 524288000)] // 500 MB Dosya Yükleme Limiti
        //[HttpPost("SaveDropZoneOneFile")]
        //public async Task<IActionResult> SaveDropZoneOneFile(IFormFile file, [FromForm] string fileGuidId, [FromForm] string tokenLoaderId, [FromForm] string userOtherPcLoginSessionId, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        // Dosya yükleme işlemi tamamlandığı zaman kod hareketi buradan başlar,  ActiveUploads sözlüğüne eklenir
        //        ActiveUploads.TryAdd(fileGuidId, new FileUploadInfo
        //        {
        //            UserOtherPcLoginSessionId = userOtherPcLoginSessionId,
        //            TokenLoaderId = tokenLoaderId,
        //            FilePath = null // Dosya yolu henüz belli değil
        //        });
        //        Console.WriteLine($"Dosya Yükleme Başladı: {JsonSerializer.Serialize(ActiveUploads)}");

        //        var command = new SaveDropZoneOneFileCommand
        //        {
        //            File = file,
        //            FileGuidId = fileGuidId,
        //            UserOtherPcLoginSessionId = userOtherPcLoginSessionId,
        //            TokenLoaderId = tokenLoaderId,
        //        };
        //        var result = await _mediator.Send(command, cancellationToken);

        //        if (result.IsSuccessful)
        //        {
        //            // Dosya yükleme işlemi başarılı olduğunda, FilePath güncellenir
        //            if (ActiveUploads.TryGetValue(fileGuidId, out var fileInfo))
        //            {
        //                fileInfo.FilePath = result.Data?.FilePath;
        //                ActiveUploads[fileGuidId] = fileInfo;
        //            }
        //            Console.WriteLine($"Dosya Yükleme Tamamlandı: {JsonSerializer.Serialize(ActiveUploads)}");
        //            return Ok(result);
        //        }
        //        else
        //        {
        //            // Dosya yükleme başarısız olduğunda, ActiveUploads'dan kaldırılır
        //            ActiveUploads.TryRemove(fileGuidId, out _);
        //            Console.WriteLine($"Dosya Yükleme Başarısız, Kayıt Silindi: {fileGuidId}");
        //            Console.WriteLine($"Dosya Yükleme Başarısız: {JsonSerializer.Serialize(ActiveUploads)}");
        //            return StatusCode(result.StatusCode, result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {  
        //        // Hata durumunda, ActiveUploads'dan kaldırılır
        //        ActiveUploads.TryRemove(fileGuidId, out _);
        //        Console.WriteLine($"Dosya Yükleme Hatası, Kayıt Silindi: {fileGuidId}");
        //        Console.WriteLine($"Hata: {ex.Message}");
        //        return StatusCode(500, Response<SaveDropZoneOneFileDto>.Fail($"Dosya yükleme sırasında bir hata oluştu: {ex.Message}", 500, true));
        //    }

        //    //---------------------------------------------------------------------------- ESKİSİ
        //    //var command = new SaveDropZoneOneFileCommand
        //    //{
        //    //    File = file,
        //    //    FileGuidId = fileGuidId,
        //    //    UserOtherPcLoginSessionId = userOtherPcLoginSessionId,
        //    //    TokenLoaderId = tokenLoaderId,
        //    //};
        //    //var result = await _mediator.Send(command, cancellationToken);

        //    //if (result.IsSuccessful)
        //    //{
        //    //    ActiveUploads.TryAdd(fileGuidId, new FileUploadInfo { UserOtherPcLoginSessionId = userOtherPcLoginSessionId, TokenLoaderId = tokenLoaderId, FilePath = result.Data?.FilePath });
        //    //    Console.WriteLine($"Yeni Dosya Kaydediliyor: {JsonSerializer.Serialize(ActiveUploads)}");

        //    //    return Ok(result);
        //    //}
        //    //else
        //    //{
        //    //    return StatusCode(result.StatusCode, result);

        //    //}
        //}

        [RequestSizeLimit(524288000)] // 500 MB Dosya Yükleme Limiti
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)] // 500 MB Dosya Yükleme Limiti
        [HttpPost("SaveDropZoneOneFile")]
        public async Task<IActionResult> SaveDropZoneOneFile(IFormFile file, [FromForm] string fileGuidId, [FromForm] string tokenLoaderId, [FromForm] string userOtherPcLoginSessionId, CancellationToken cancellationToken)
        {
            try
            {
                // Dosya yükleme işlemi tamamlandığı zaman kod hareketi buradan başlar,  ActiveUploads sözlüğüne eklenir
                ActiveUploads.TryAdd(fileGuidId, new FileUploadInfo
                {
                    UserOtherPcLoginSessionId = userOtherPcLoginSessionId,
                    TokenLoaderId = tokenLoaderId,
                    FilePath = null // Dosya yolu henüz belli değil
                });
                Console.WriteLine($"Dosya Yükleme Başladı: {JsonSerializer.Serialize(ActiveUploads)}");

                var command = new SaveDropZoneOneFileCommand
                {
                    File = file,
                    FileGuidId = fileGuidId,
                    UserOtherPcLoginSessionId = userOtherPcLoginSessionId,
                    TokenLoaderId = tokenLoaderId,
                };
                // Bu satırda Dosya indirmesi tamamlanmış ve Klasöre Dosyayı yerleştiriyor.
                var result = await _mediator.Send(command, cancellationToken);

                if (result.IsSuccessful)
                {
                    // Dosya yükleme işlemi başarılı olduğunda, FilePath güncellenir
                    if (ActiveUploads.TryGetValue(fileGuidId, out var fileInfo))
                    {
                        fileInfo.FilePath = result.Data?.FilePath;
                        ActiveUploads[fileGuidId] = fileInfo;
                    }
                    Console.WriteLine($"Dosya Yükleme Tamamlandı: {JsonSerializer.Serialize(ActiveUploads)}");
                    return Ok(result);
                }
                else
                {
                    // Dosya yükleme başarısız olduğunda, ActiveUploads'dan kaldırılır
                    ActiveUploads.TryRemove(fileGuidId, out _);
                    Console.WriteLine($"Dosya Yükleme Başarısız, Kayıt Silindi: {fileGuidId}");
                    Console.WriteLine($"Dosya Yükleme Başarısız: {JsonSerializer.Serialize(ActiveUploads)}");
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda, ActiveUploads'dan kaldırılır
                ActiveUploads.TryRemove(fileGuidId, out _);
                Console.WriteLine($"Dosya Yükleme Hatası, Kayıt Silindi: {fileGuidId}");
                Console.WriteLine($"Hata: {ex.Message}");
                return StatusCode(500, Response<SaveDropZoneOneFileDto>.Fail($"Dosya yükleme sırasında bir hata oluştu: {ex.Message}", 500, true));
            }         
        }

        //[HttpPost("CancelIncompleteFileTransfer")]
        //public IActionResult CancelIncompleteFileTransfer([FromBody] CancelFileTransferRequest request)
        //{
        //    if (ActiveUploads.TryRemove(request.GuidId!, out var fileInfo))
        //    {
        //        // Dosya yükleme işlemini iptal et ve kaydedilen dosyayı sil
        //        if (System.IO.File.Exists(fileInfo.FilePath))
        //        {
        //            System.IO.File.Delete(fileInfo.FilePath);
        //            Console.WriteLine($"CancelIncompleteFileTransfer. Aktarım iptal edildi. Dosya silindi, Sözlükten silindi: {request.GuidId}");
        //            Console.WriteLine($"Çıkarılandan Sonraki Kalanlar Listesi: {JsonSerializer.Serialize(ActiveUploads)}");

        //            return Ok(Response<string>.Success("CancelIncompleteFileTransfer. Aktarım iptal edildi. Dosya silindi, Sözlükten silindi.", 200));
        //        }
        //    }
        //    else
        //    {
        //        // If the file is not in ActiveUploads, try to delete it as a downloaded file
        //        var basePath = _configuration.GetValue<string>("FileStorage:BasePath6");
        //        var filePath = Path.Combine(basePath!, $"{request.GuidId}.*");
        //        var files = Directory.GetFiles(Path.GetDirectoryName(filePath)!, Path.GetFileName(filePath));

        //        if (files.Length > 0)
        //        {
        //            System.IO.File.Delete(files[0]);
        //            Console.WriteLine($"DownloadedFile. İndirilmiş dosya silindi, Sözlükten silindi: {request.GuidId}");
        //            return Ok(Response<string>.Success("DownloadedFile. İndirilmiş dosya silindi + Sözlükten silindi.", 200));
        //        }
        //        else
        //        {
        //            Console.WriteLine($"DownloadedFile. Sözlükte bulunamadı: {request.GuidId}");
        //            return NotFound(Response<string>.Fail("DownloadedFile. Sözlükte bulunamadı.", 404, true));
        //        }
        //    }

        //    // This line should never be reached, but we'll keep it as a fallback
        //    Console.WriteLine($"CancelIncompleteFileTransfer. Sözlükte bulunamadı: {request.GuidId}");
        //    return NotFound(Response<string>.Fail("CancelIncompleteFileTransfer. Sözlükte bulunamadı.", 404, true));

        //}

        [HttpPost("CancelIncompleteFileTransfer")]
        public IActionResult CancelIncompleteFileTransfer([FromBody] CancelFileTransferRequest request)
        {
            // Debug için mevcut uploads listesini yazdır
            Console.WriteLine($"Mevcut ActiveUploads Listesi: {JsonSerializer.Serialize(ActiveUploads)}");
            Console.WriteLine($"Aranan GuidId: {request.GuidId}");

            // İşlem 1: Aktif Yükleme İptali
            bool isActiveUploadRemoved = ActiveUploads.TryRemove(request.GuidId!, out var fileInfo);
            Console.WriteLine($"ActiveUploads.TryRemove sonucu: {isActiveUploadRemoved}");

            if (isActiveUploadRemoved)
            {
                try
                {
                    Console.WriteLine($"FileInfo Path: {fileInfo?.FilePath}");

                    if (fileInfo?.FilePath != null)
                    {
                        bool fileExists = System.IO.File.Exists(fileInfo.FilePath);
                        Console.WriteLine($"Dosya mevcut mu: {fileExists}");

                        if (fileExists)
                        {
                            System.IO.File.Delete(fileInfo.FilePath);
                            Console.WriteLine("Dosya silindi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }

                return Ok(Response<string>.Success("Aktarım başarıyla iptal edildi.", 200));
            }

            // İşlem 2: İndirilmiş Dosya Silme
            try
            {
                var basePath = _configuration.GetValue<string>("FileStorage:BasePath6");
                Console.WriteLine($"BasePath: {basePath}");

                var filePath = Path.Combine(basePath!, $"{request.GuidId}.*");
                Console.WriteLine($"Aranacak dosya pattern: {filePath}");

                var directoryPath = Path.GetDirectoryName(filePath)!;
                var searchPattern = Path.GetFileName(filePath);
                Console.WriteLine($"Directory: {directoryPath}");
                Console.WriteLine($"Search Pattern: {searchPattern}");

                var files = Directory.GetFiles(directoryPath, searchPattern);
                Console.WriteLine($"Bulunan dosya sayısı: {files.Length}");

                if (files.Length > 0)
                {
                    System.IO.File.Delete(files[0]);
                    Console.WriteLine($"İndirilmiş dosya silindi: {files[0]}");
                    return Ok(Response<string>.Success("İndirilmiş dosya silindi.", 200));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"İndirilmiş dosya silme hatası: {ex.Message}");
            }

            return Ok(Response<string>.Success("İşlem tamamlandı.", 200));
        }





        #endregion
        // Dosya Yükleme Silme Çalışıyor Versiyon 1 END


        [HttpPost("CancelUpload")]
        public IActionResult CancelUpload([FromBody] CancelUploadRequest request)
        {
            if (string.IsNullOrEmpty(request.FileGuidId))
            {
                return BadRequest(Response<string>.Fail("FileGuidId is required.", 400, true));
            }

            if (ActiveUploads.TryRemove(request.FileGuidId, out var fileInfo))
            {
                // Dosya yükleme işlemi devam ediyorsa, ilgili kaynakları temizle
                // Örneğin, geçici dosyaları silme işlemi burada yapılabilir
                if (!string.IsNullOrEmpty(fileInfo.FilePath) && System.IO.File.Exists(fileInfo.FilePath))
                {
                    try
                    {
                        System.IO.File.Delete(fileInfo.FilePath);
                        Console.WriteLine($"Temporary file deleted: {fileInfo.FilePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting temporary file: {ex.Message}");
                    }
                }

                Console.WriteLine($"Upload cancelled for FileGuidId: {request.FileGuidId}");
                return Ok(Response<string>.Success("Upload cancelled successfully.", 200));
            }
            else
            {
                Console.WriteLine($"No active upload found for FileGuidId: {request.FileGuidId}");
                return NotFound(Response<string>.Fail("No active upload found with the given FileGuidId.", 404, true));
            }
        }

        //documentViewerf1menu7 CreateForm ile Kaydedilmiş Dosya bilgilerini Kaydedecek. BasePath6 daki o dosyayı, BasePath7 ye taşıyacak, Taşıma bitince BasePath6 daki dosyayı silecek.
        [HttpPost("CreateNewDocumentf1menu7")]
        public async Task<IActionResult> CreateNewDocumentf1menu7([FromBody] CreateNewDocumentf1menu7Command command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccessful
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpGet("GetAllFilesf1menu7")]
        public async Task<IActionResult> GetAllFilesf1menu7()
        {
            //var command = new GetAllFilesf1menu7Command();
            //var result = await _mediator.Send(command);

            //return result.IsSuccessful
            //    ? Ok(result)
            //    : BadRequest(result);
            try
            {
                var records = await _repository.GetAllAsync();
                var response = Response<List<DocumentViewerf1menu7>>.Success(records, 200);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = Response<List<DocumentViewerf1menu7>>.Fail(ex.Message, 500, true);
                return BadRequest(response);
            }
        }


        [HttpPost("UpdateByIdf1menu7")]
        public async Task<IActionResult> UpdateByIdf1menu7([FromBody] UpdateByIdf1menu7Command command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateSortIndexf1menu7")]
        public async Task<IActionResult> UpdateSortIndexf1menu7([FromBody] UpdateSortIndexf1menu7Command command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccessful
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpPost("Previewf1menu7")]
        public async Task<IActionResult> Previewf1menu7([FromBody] DocumentViewerServiceMessageDto previewDto)
        {
            try
            {
                // Servisin beklediği DTO sınıfını oluştur ve verileri ata
                var previewServiceDto = new DocumentViewerServiceMessageDto
                {
                    Id = previewDto.Id,
                    Opener = previewDto.Opener,
                    DbTableName = "DocumentViewersf1menu7",
                    SourceFilePathName1 = "FileStorage",
                    SourceFilePathName2 = "BasePath7",
                    CopyTargetFilePath = "BasePath5",
                    PresentationFilePath = "StaticFiles"
                };

                var result = await _documentViewerPreviewService.PreviewDocumentTableDocumentViewersf1menu7Async(
              previewServiceDto,
              CancellationToken.None);

                return Ok(Response<DocumentViewerPreviewResponseDto>.Success(result, 200));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<string>.Fail(ex.Message, 500, true));
            }
        }

        [HttpPost("Clearf1menu7")]
        public IActionResult Clearf1menu7()
        {
            try
            {
                //string projectRootPath = Directory.GetCurrentDirectory();
                //string cleanupPath = Path.Combine(projectRootPath, "aaaStaticFiles", "f1menu7");
                var cleanupPath = _configuration.GetValue<string>("FileStorage:BasePath8");
                int deletedCount = 0;

                if (Directory.Exists(cleanupPath))
                {
                    // Tüm dosya ve klasörleri al
                    var directories = Directory.GetDirectories(cleanupPath);
                    var files = Directory.GetFiles(cleanupPath);

                    // Dosyaları sil
                    foreach (var file in files)
                    {
                        try
                        {
                            System.IO.File.Delete(file);
                            deletedCount++;
                        }
                        catch (IOException)
                        {
                            // Dosya kullanımda ise biraz bekle ve tekrar dene
                            Thread.Sleep(100);
                            System.IO.File.Delete(file);
                            deletedCount++;
                        }
                    }

                    // Klasörleri sil
                    foreach (var dir in directories)
                    {
                        try
                        {
                            Directory.Delete(dir, true); // true parametresi ile içindeki tüm dosyalarla birlikte sil
                            deletedCount++;
                        }
                        catch (IOException)
                        {
                            // Klasör kullanımda ise biraz bekle ve tekrar dene
                            Thread.Sleep(100);
                            Directory.Delete(dir, true);
                            deletedCount++;
                        }
                    }
                }

                return Ok(Response<int>.Success(deletedCount, 200));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<int>.Fail(ex.Message, 500, true));
            }
        }


    }

    #region Dosya Yükle, Yüklemesi Devam Eden İşlemi İptal Et, Yüklenmiş Dosyayı Sil. Classları
    public class FileUploadInfo
    {
        public string? UserOtherPcLoginSessionId { get; set; }
        public string? TokenLoaderId { get; set; }
        public string? FilePath { get; set; }
    }

    public class CancelFileTransferRequest
    {
        public string? GuidId { get; set; }
    }

    public class DeleteDownloadedFilesRequest
    {
        public string? GuidId { get; set; }
    }
    public class CancelUploadRequest
    {
        public string? FileGuidId { get; set; }
    }

    #endregion
}
