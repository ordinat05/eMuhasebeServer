using eMuhasebeServer.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace eMuhasebeServer.WebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class SignalRTestController : ControllerBase
    //{
    //    private readonly IHubContext<DocumentHub> _hubContext;

    //    public SignalRTestController(IHubContext<DocumentHub> hubContext)
    //    {
    //        _hubContext = hubContext;
    //    }

    //    [HttpPost("test-excel-conversion")]
    //    public async Task<IActionResult> TestExcelConversion([FromBody] HtmlConversionRequestDto conversionRequest)
    //    {
    //        // SignalR üzerinden Windows Form'a gönder
    //        await _hubContext.Clients.Group(conversionRequest.MachineId!).SendAsync("ConvertExcelToHtml", conversionRequest);

    //        // Gelen request'i aynen geri dön
    //        return Ok(conversionRequest);
    //    }
    //}
}
