using eMuhasebeServer.Application.Features.FileContent2s.DeleteFileContent2sById;
using eMuhasebeServer.Application.Features.FileContent2s.GetAllByFileContentTableRowId;
using eMuhasebeServer.Application.Features.FileContent2s.GetAllFileContent2s;
using eMuhasebeServer.Application.Features.FileContent2s.SaveAllFileContent2s;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers
{
    public sealed class FileContent2sController : ApiController
    {
        public FileContent2sController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFileContent2sQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("byfilecontenttablerow/{fileContentTableRowId}")]
        public async Task<IActionResult> GetAllByFileContentTableRowId(Guid fileContentTableRowId, CancellationToken cancellationToken)
        {
            var request = new GetAllByFileContentTableRowIdCommand(fileContentTableRowId);
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost()]
        public async Task<IActionResult> SaveAll([FromBody] SaveAllFileContent2sCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById([FromBody] DeleteFileContent2ByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}