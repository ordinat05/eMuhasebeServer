using eMuhasebeServer.Application.Features.SideBarLeftMenu.CreateSideBarLeft;
using eMuhasebeServer.Application.Features.SideBarLeftMenu.DeleteSideBarLeftById;
using eMuhasebeServer.Application.Features.SideBarLeftMenu.GetAllSideBarLeft;
using eMuhasebeServer.Application.Features.SideBarLeftMenu.SaveAllSideBarLeft;
using eMuhasebeServer.Application.Features.SideBarLeftMenu.UpdateSideBarLeft;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class SideBarLeftController : ApiController
{
    public SideBarLeftController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllSideBarLeftQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSideBarLeftCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteSideBarLeftByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAll([FromBody] SaveAllSideBarLeftCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSideBarLeftCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

}

