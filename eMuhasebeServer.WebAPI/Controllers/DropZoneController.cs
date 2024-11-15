using eMuhasebeServer.Application.Features.DropZones.DropZonePreviewDwg;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace eMuhasebeServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropZoneController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DropZoneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("previewdwg")]
        public async Task<IActionResult> PreviewDwg(IFormFile file)
        {
            var command = new DropZonePreviewDwgCommand(file);
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
            {
                return Ok(new
                {
                    data = result.Data,
                    errorMessages = (IEnumerable<string>?)null,
                    isSuccessful = true
                });
            }
            else
            {
                return BadRequest(new
                {
                    data = (string?)null,
                    errorMessages = result.ErrorMessages,
                    isSuccessful = false
                });
            }
        }
    }
}
