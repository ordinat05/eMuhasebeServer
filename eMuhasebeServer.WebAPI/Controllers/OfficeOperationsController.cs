using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Application.Features.OfficeOperations.Excel.ExcelConvertHtm;
using MediatR;
using System.IO.Compression;
using eMuhasebeServer.Application.Features.OfficeOperations.Word.WordConvertHtm;



namespace eMuhasebeServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeOperationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;


        public OfficeOperationsController(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        [HttpPost("exceltohtm")]
        public async Task<IActionResult> Exceltohtm(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Lütfen geçerli bir dosya yükleyin.");

            try
            {
              
                var (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName) = await _fileService.UploadFileExcelSrvAsync(file, "excel_uploads", CancellationToken.None);

               
                var result = await _mediator.Send(new ExcelConvertHtmCommand(folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName));

                if (!result.IsSuccessful)
                    return StatusCode(500, result.ErrorMessages);

                return Ok(new
                {
                    IsSuccessful = result.IsSuccessful,
                    Data = result.Data,
                    StatusCode = result.StatusCode,
                    ErrorMessages = result.ErrorMessages
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        } 
        
        [HttpPost("exceltohtmZip")]
        public async Task<IActionResult> ExceltohtmZip(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Lütfen geçerli bir dosya yükleyin.");

            try
            {
              
                var (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName) = await _fileService.UploadFileExcelSrvAsync(file, "excel_uploads", CancellationToken.None);

               
                var result = await _mediator.Send(new ExcelConvertHtmCommand(folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName));

                if (!result.IsSuccessful)
                    return StatusCode(500, result.ErrorMessages);

                var htmOutputDirectory = Path.GetDirectoryName(result.Data);

                if (string.IsNullOrEmpty(htmOutputDirectory) || !Directory.Exists(htmOutputDirectory))
                {
                    return StatusCode(500, "HTML çıktısı için dizin oluşturulamadı.");
                }
                // HTML dosyalarını ve ilgili kaynakları zip dosyasına sıkıştır
                var zipPath = Path.Combine(Path.GetTempPath(), $"{fileGuidIdName}.zip");
                ZipFile.CreateFromDirectory(htmOutputDirectory, zipPath);

                // Zip dosyasını oku ve byte dizisine dönüştür
                byte[] zipBytes = System.IO.File.ReadAllBytes(zipPath);

                // Geçici zip dosyasını sil
                System.IO.File.Delete(zipPath);

                // Zip dosyasını istemciye 'Content-Disposition' başlığı ile gönder START
                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    FileName = $"{fileGuidIdName}.zip",
                    Inline = false // İndirmeyi tetiklemek için
                };
                Response.Headers["Content-Disposition"] = $"attachment; filename=\"{fileGuidIdName}.zip\"";
                // Zip dosyasını istemciye 'Content-Disposition' başlığı ile gönder END

                // Zip dosyasını istemciye gönder
                return File(zipBytes, "application/zip", $"{fileGuidIdName}.zip");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }




        [HttpPost("wordtohtm")]
        public async Task<IActionResult> Wordtohtm(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Lütfen geçerli bir dosya yükleyin.");

            try
            {
              
                var (folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName) = await _fileService.UploadFileWordSrvAsync(file, "excel_uploads", CancellationToken.None);

               
                var result = await _mediator.Send(new WordConvertHtmCommand(folderPathAndFileName, fileNameAndFileExtension, uploadPath, fileGuidIdName));

                if (!result.IsSuccessful)
                    return StatusCode(500, result.ErrorMessages);

                return Ok(new
                {
                    IsSuccessful = true,
                    Data = result.Data,
                    StatusCode = 200,
                    ErrorMessages = new List<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}

