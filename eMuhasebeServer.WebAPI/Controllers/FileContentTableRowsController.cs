using eMuhasebeServer.Application.Features.FileContentTableRows.CreateFileContentTableRow;
using eMuhasebeServer.Application.Features.FileContentTableRows.DeleteFileContentTableRowById;
using eMuhasebeServer.Application.Features.FileContentTableRows.GetAllFileContentTableRows;
using eMuhasebeServer.Application.Features.FileContentTableRows.UpdateFileContentTableRow;
using eMuhasebeServer.Application.Features.FileContentTableRows.UpdateSortIndex;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers
{
    public sealed class FileContentTableRowsController : ApiController
    {
        public FileContentTableRowsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllFileContentTableRowsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFileContentTableRowCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById([FromBody] DeleteFileContentTableRowByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateFileContentTableRowCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSortIndex([FromBody] List<UpdateSortIndexRequest> orderUpdates, CancellationToken cancellationToken)
        {
            var command = new UpdateSortIndexCommand(orderUpdates);
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccessful)
                return Ok(result);

            return BadRequest(result);
        }
    }
}