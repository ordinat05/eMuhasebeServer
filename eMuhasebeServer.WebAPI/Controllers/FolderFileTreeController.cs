using eMuhasebeServer.Application.Features.FolderFileTrees.CreateFolderFileTree;
using eMuhasebeServer.Application.Features.FolderFileTrees.DeleteFolderFileTree;
using eMuhasebeServer.Application.Features.FolderFileTrees.GetAllFolderFileTree;
using eMuhasebeServer.Application.Features.FolderFileTrees.UpdateFolderFileTree;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

//[Route("api/[controller]")]
//[ApiController]
public sealed class FolderFileTreeController : ApiController
{

    public FolderFileTreeController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllFolderFileTreeQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFolderFileTreeCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateFolderFileTreeCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteFolderFileTreeCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
