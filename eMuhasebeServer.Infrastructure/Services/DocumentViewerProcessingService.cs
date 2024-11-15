using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Entities.Dtos.DocumentViewerDto;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;

namespace eMuhasebeServer.Infrastructure.Services
{
    public class DocumentViewerProcessingService : IDocumentViewerProcessingService
    {
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly IExcelService _excelService;
        private readonly IWordService _wordService;
        private readonly IServiceProvider _serviceProvider;


        public DocumentViewerProcessingService(IHostEnvironment environment, IConfiguration configuration, IExcelService excelService, IWordService wordService,
    IServiceProvider serviceProvider)
        {
            _environment = environment;
            _configuration = configuration;
            _excelService = excelService;
            _wordService = wordService;
            _serviceProvider = serviceProvider;
        }
        public string GetBasePath()
        {
            return _configuration.GetValue<string>("FileStorage:BasePath") ?? "Varsayilan/Dosya/Yolu/Bulunamadı";
        }
        public string GetBasePath5()
        {
            return _configuration.GetValue<string>("FileStorage:BasePath5") ?? "Default/File/Path/NotFound";
        }       
        public string GetBasePath7()
        {
            return _configuration.GetValue<string>("FileStorage:BasePath7") ?? "Default/File/Path/NotFound";
        }      
        public string GetBasePath8()
        {
            return _configuration.GetValue<string>("FileStorage:BasePath8") ?? "Default/File/Path/NotFound";
        }

        private string GetFullFileUrl(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document, string extension)
        {
            // FilePathName1 ve FilePathName2'yi kullanarak tam dosya yolunu oluştur
            var basePath = _configuration.GetValue<string>($"{previewDto.SourceFilePathName1}:{previewDto.SourceFilePathName2}");
            return $"{basePath}/{document.Id}{extension}";
        }


        #region HTM viewer
        public async Task<DocumentViewerPreviewResponseDto> ProcessXlsHtmAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();
                var fileNameAndFileExtension = Path.GetFileName(catchSourceFile);
                string newFolderAndFileName = Guid.NewGuid().ToString("N");

                if (string.IsNullOrEmpty(catchSourceFile))
                {
                    throw new Exception("Kaynak dosya bulunamadı.");
                }
                if (string.IsNullOrEmpty(fileNameAndFileExtension))
                {
                    throw new Exception("Dosya adı ve Uzantısı boş olamaz.");
                }

                //------------------------------------ Aşağıdaki metod yerine 
                //var  htmOutputDirectory = _excelService.StaticFilesConvertExcelToHtmlSrv3(catchSourceFile, fileNameAndFileExtension, copyTargetFilePath, newFolderAndFileName);

                //------------------------------------ Burada SignalR Windows Form a Gönderiyoruz.

                //SignalR hub'ına erişim için bir referans gerekiyor
                var hubContext = _serviceProvider.GetRequiredService<IHubContext<DocumentHub>>();
                var machineId = "0123456789";
                // HtmlConversionRequestDto nesnesini doldur
                var conversionExcelRequest = new HtmlConversionRequestDto
                {
                    CatchSourceFile = catchSourceFile,
                    FileNameAndExtension = fileNameAndFileExtension,
                    CopyTargetFilePath = copyTargetFilePath,
                    NewFolderAndFileName = newFolderAndFileName,
                    MachineId = machineId,
                };




                //WinForm'a Excel dönüştürme isteğini gönder
                await hubContext.Clients.Group(machineId).SendAsync("ConvertExcelToHtml", conversionExcelRequest);


                // Burada WinForm'dan gelecek yanıtı beklemek için TaskCompletionSource kullanabilirsiniz
                var tcs = new TaskCompletionSource<string>();

                // WinForm'dan gelen yanıtı bekle ve sonucu döndür
                var htmOutputDirectory = await tcs.Task;


                //------------------------------------ Windows Form SignalR dan gelen cevap.
                //var htmOutputDirectory = "";
                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = ".htm",
                    Opener = previewDto.Opener,
                    DestinationPath = htmOutputDirectory.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                //return Task.FromResult(response);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya dönüştürme hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessDocHtmAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();
                var fileNameAndFileExtension = Path.GetFileName(catchSourceFile);

                if (string.IsNullOrEmpty(catchSourceFile))
                {
                    throw new Exception("Kaynak dosya bulunamadı.");
                }
                if (string.IsNullOrEmpty(fileNameAndFileExtension))
                {
                    throw new Exception("Dosya adı ve Uzantısı boş olamaz.");
                }

                string newFolderAndFileName = Guid.NewGuid().ToString("N");

                var htmOutputDirectory = _wordService.StaticFilesConvertWordToHtmlSrv3(
                    catchSourceFile,
                    fileNameAndFileExtension,
                    copyTargetFilePath,
                    newFolderAndFileName);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = ".htm",
                    Opener = previewDto.Opener,
                    DestinationPath = htmOutputDirectory.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya dönüştürme hatası: {ex.Message}");
            }
        }
        #endregion  

       #region HTM viewer
        //public Task<DocumentViewerPreviewResponseDto> ProcessXlsHtmAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        //{
        //    try
        //    {
        //        var sourceFilePathName2 = GetBasePath7();
        //        var copyTargetFilePath = GetBasePath8();
        //        var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();
        //        var fileNameAndFileExtension = Path.GetFileName(catchSourceFile);

        //        if (string.IsNullOrEmpty(catchSourceFile))
        //        {
        //            throw new Exception("Kaynak dosya bulunamadı.");
        //        }
        //        if (string.IsNullOrEmpty(fileNameAndFileExtension))
        //        {
        //            throw new Exception("Dosya adı ve Uzantısı boş olamaz.");
        //        }

        //        string newFolderAndFileName = Guid.NewGuid().ToString("N") ;

        //        var htmOutputDirectory = _excelService.StaticFilesConvertExcelToHtmlSrv3(catchSourceFile, fileNameAndFileExtension, copyTargetFilePath, newFolderAndFileName);

        //        var response = new DocumentViewerPreviewResponseDto
        //        {
        //            FileExtension = ".htm",
        //            Opener = previewDto.Opener,
        //            DestinationPath = htmOutputDirectory.Split("tammerkezStaticFiles\\").Last(),
        //            PreviewUrl = "iframe oluşturulursa doldurulacak."
        //        };

        //        return Task.FromResult(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Dosya dönüştürme hatası: {ex.Message}");
        //    }
        //}

        //public Task<DocumentViewerPreviewResponseDto> ProcessDocHtmAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        //{
        //    try
        //    {
        //        var sourceFilePathName2 = GetBasePath7();
        //        var copyTargetFilePath = GetBasePath8();
        //        var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();
        //        var fileNameAndFileExtension = Path.GetFileName(catchSourceFile);

        //        if (string.IsNullOrEmpty(catchSourceFile))
        //        {
        //            throw new Exception("Kaynak dosya bulunamadı.");
        //        }
        //        if (string.IsNullOrEmpty(fileNameAndFileExtension))
        //        {
        //            throw new Exception("Dosya adı ve Uzantısı boş olamaz.");
        //        }

        //        string newFolderAndFileName = Guid.NewGuid().ToString("N");

        //        var htmOutputDirectory = _wordService.StaticFilesConvertWordToHtmlSrv3(
        //            catchSourceFile,
        //            fileNameAndFileExtension,
        //            copyTargetFilePath,
        //            newFolderAndFileName);

        //        var response = new DocumentViewerPreviewResponseDto
        //        {
        //            FileExtension = ".htm",
        //            Opener = previewDto.Opener,
        //            DestinationPath = htmOutputDirectory.Split("tammerkezStaticFiles\\").Last(),
        //            PreviewUrl = "iframe oluşturulursa doldurulacak."
        //        };

        //        return Task.FromResult(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Dosya dönüştürme hatası: {ex.Message}");
        //    }
        //}
        #endregion

        #region Google viewer
        public Task<DocumentViewerPreviewResponseDto> ProcessXlsGoogleAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }
        // TODO SEE
        public Task<DocumentViewerPreviewResponseDto> ProcessXlsxGoogleAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessDocGoogleAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessDocxGoogleAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }
        #endregion

        #region Microsoft viewer
        public Task<DocumentViewerPreviewResponseDto> ProcessXlsMicrosoftAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }
        // TODO SEE
        public Task<DocumentViewerPreviewResponseDto> ProcessXlsxMicrosoftAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessDocMicrosoftAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessDocxMicrosoftAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------
        #region Picture Viewer
        public Task<DocumentViewerPreviewResponseDto> ProcessPngAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessBmpAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessJpgAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessJpegAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public Task<DocumentViewerPreviewResponseDto> ProcessXpsAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }
        #endregion

        #region PDF Viewer
        public Task<DocumentViewerPreviewResponseDto> ProcessPdfAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }
        #endregion

        #region Txt Viewer
        public Task<DocumentViewerPreviewResponseDto> ProcessTxtAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }
        #endregion

        #region Lsp Viewer
        public Task<DocumentViewerPreviewResponseDto> ProcessLspAsync(DocumentViewerServiceMessageDto previewDto, DocumentViewerf1menu7 document)
        {
            try
            {
                var sourceFilePathName2 = GetBasePath7();
                var copyTargetFilePath = GetBasePath8();
                var catchSourceFile = Directory.GetFiles(sourceFilePathName2, $"{previewDto.Id}.*").FirstOrDefault();

                string originalFileExtension = Path.GetExtension(document.Filename!);
                string newFileName = Guid.NewGuid().ToString("N") + originalFileExtension;
                string destinationPathAndFileExtension = Path.Combine(copyTargetFilePath, newFileName);

                File.Copy(catchSourceFile!, destinationPathAndFileExtension, true);

                var response = new DocumentViewerPreviewResponseDto
                {
                    FileExtension = originalFileExtension,
                    Opener = previewDto.Opener,
                    DestinationPath = destinationPathAndFileExtension.Split("tammerkezStaticFiles\\").Last(),
                    PreviewUrl = "iframe oluşturulursa doldurulacak."
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kopyalama hatası: {ex.Message}");
            }
        }
        #endregion

    }
}