using eMuhasebeServer.Application.InterfaceService;
using Hangfire.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace eMuhasebeServer.Infrastructure.Services;


public class DocumentHub : Hub, IDocumentHub
{
    private readonly ILogger<DocumentHub> _logger;
    // Static dictionary tanımı
    private static readonly ConcurrentDictionary<string, HtmlConversionRequestDto> _lastSentMessages =
        new ConcurrentDictionary<string, HtmlConversionRequestDto>();

    public DocumentHub(ILogger<DocumentHub> logger)
    {
        _logger = logger;
    }
    #region Makine Kayıt/Bağlantı:
    // - RegisterMachine: Bilgisayarın kaydını alır ve gruba ekler
    // - OnConnectedAsync: Bağlantı kurulduğunda log tutar
    // - GetConnectedMachines: Bağlı bilgisayarları listeler
    public async Task RegisterMachine(string machineId, string installPath)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, machineId);
        _logger.LogInformation($"Machine registered: {machineId} at {installPath}");
    }
    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation($"Client connected: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }
    //public async Task<List<string>> GetConnectedMachines()
    //{
    //    var connections = Groups.GetGroupsAsync(Context.ConnectionId);
    //    _logger.LogInformation("Connected machines listed");
    //    return await connections;
    //}
    #endregion

    #region Dosya İşlemleri Bildirimleri:
    //- NotifyFileUploadStarted: Dosya yüklemeye başlandığında
    //- NotifyFileUploadProgress: Yükleme ilerleme durumu
    //- NotifyFileUploadCompleted: Yükleme tamamlandığında
    //- NotifyFileDeleted: Dosya silindiğinde
    public async Task NotifyFileUploadStarted(string machineId, string fileName)
    {
        await Clients.Group(machineId).SendAsync("FileUploadStarted", fileName);
    }

    public async Task NotifyFileUploadProgress(string machineId, string fileName, int percentage)
    {
        await Clients.Group(machineId).SendAsync("FileUploadProgress", fileName, percentage);
    }

    public async Task NotifyFileUploadCompleted(string machineId, string fileName, string filePath)
    {
        await Clients.Group(machineId).SendAsync("FileUploadCompleted", fileName, filePath);
    }

    public async Task NotifyFileDeleted(string machineId, string fileName)
    {
        await Clients.Group(machineId).SendAsync("FileDeleted", fileName);
    }
    #endregion

    #region Dönüştürme İşlemleri:
    //- NotifyConversionStarted: Dönüştürme başladığında
    //- NotifyConversionProgress: Dönüştürme ilerleme durumu
    //- NotifyConversionCompleted: Dönüştürme tamamlandığında
    //- NotifyConversionError: Dönüştürme hatası olduğunda
    public async Task NotifyConversionStarted1(string machineId, string documentId)
    {
        await Clients.Group(machineId).SendAsync("ConversionStarted", documentId);
    }

    public async Task NotifyConversionProgress(string machineId, string documentId, int percentage)
    {
        await Clients.Group(machineId).SendAsync("ConversionProgress", documentId, percentage);
    }

    public async Task NotifyConversionCompleted(string machineId, string documentId, string outputPath)
    {
        await Clients.Group(machineId).SendAsync("ConversionCompleted", documentId, outputPath);
    }

    public async Task NotifyConversionError(string machineId, string documentId, string error)
    {
        await Clients.Group(machineId).SendAsync("ConversionError", documentId, error);
    }
    //--------------------------------------- Gönderilen Datayı yakalamak START

    // Clients.Group'a göndermeden önce datayı yakalayalım
    public async Task NotifyConversionStarted(string machineId, HtmlConversionRequestDto request)
    {

        // Dictionary'ye kaydet
        _lastSentMessages.AddOrUpdate(machineId, request, (key, old) => request);

        // Normal SignalR gönderimi
        await Clients.Group(machineId).SendAsync("ConvertExcelToHtml", request);
    }

    // Son gönderilen mesajı almak için metod
    public static HtmlConversionRequestDto? GetLastSentMessage(string machineId)
    {
        _lastSentMessages.TryGetValue(machineId, out var message);
        return message;
    }
    //--------------------------------------- Gönderilen Datayı yakalamak END


    //---------------------------------------
    //public async Task RequestWordToHtmlConversion(string machineId, HtmlConversionRequestDto request)
    //{
    //    // WinForm'a Word dönüştürme isteği gönder
    //    await Clients.Group(machineId).SendAsync("ConvertWordToHtml", request);
    //}
    //// WinForm'dan gelen Word sonucu alan metod
    //public async Task NotifyWordHtmlConversionResult(
    //     string machineId,
    //     string documentId,
    //     string htmPath,
    //     bool success,
    //     string? error = null)
    //{
    //    if (success)
    //    {
    //        await Clients.Group(machineId).SendAsync("HtmlWordConversionCompleted", documentId, htmPath);
    //    }
    //    else
    //    {
    //        await Clients.Group(machineId).SendAsync("HtmlWordConversionError", documentId, error);
    //    }
    //}
    ////---------------------------------------
    //public async Task RequestExcelToHtmlConversion(string machineId, HtmlConversionRequestDto request)
    //{
    //    // WinForm'a Excel dönüştürme isteği gönder
    //    await Clients.Group(machineId).SendAsync("ConvertExcelToHtml", request);
    //}
    //// WinForm'dan gelen Excel sonucu alan metod
    //public async Task NotifyExcelHtmlConversionResult(
    //    string machineId,
    //    string documentId,
    //    string htmPath,
    //    bool success,
    //    string? error = null)
    //{
    //    if (success)
    //    {
    //        await Clients.Group(machineId).SendAsync("HtmlExcelConversionCompleted", documentId, htmPath);
    //    }
    //    else
    //    {
    //        await Clients.Group(machineId).SendAsync("HtmlExcelConversionError", documentId, error);
    //    }
    //}
    #endregion
}
