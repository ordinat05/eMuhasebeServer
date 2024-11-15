using eMuhasebeServer.Application.Features.KatCizelgeler.CreateKatCizelge;
using eMuhasebeServer.Application.Features.KatCizelgeler.GetAllKatCizelge;
using eMuhasebeServer.Application.Features.KatCizelgeler.KatCizelgeHeaderSearchPatterns;
using eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;
using eMuhasebeServer.Domain.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TS.Result;
using MediatR;

namespace eMuhasebeServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KatCizelgeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public KatCizelgeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KatCizelgeOlusturmaDto request, CancellationToken cancellationToken)
        {
            var command = new CizelgeOlusturCommand { KatCizelgeOlusturmaDto = request };
            Result<NoDataDto> result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccessful)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            Result<List<KatCizelgeListDto>> result = await _mediator.Send(new GetAllKatCizelgeQuery(), cancellationToken);

            if (result.IsSuccessful)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessages);
        }
        [HttpPost("KatCizelgeHeaderSearchPattern")]
        public async Task<IActionResult> SearchKatCizelgeHeader([FromBody] KatCizelgeHeaderSearchPatternQuery request, CancellationToken cancellationToken)
        {
            Result<List<KatCizelgeListDto>> result = await _mediator.Send(request, cancellationToken);

            if (result.IsSuccessful)
            {
                return Ok(new
                {
                    data = result.Data,
                    errorMessages = (IEnumerable<string>?)null,
                    isSuccessful = true
                });
            }

            return BadRequest(new
            {
                data = (IEnumerable<KatCizelgeListDto>?)null,
                errorMessages = result.ErrorMessages,
                isSuccessful = false
            });
        }

    }    
}
