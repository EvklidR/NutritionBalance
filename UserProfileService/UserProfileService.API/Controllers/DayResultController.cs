using Microsoft.AspNetCore.Mvc;
using MediatR;
using UserProfileService.Application.UseCases.DayResult;
using UserProfileService.Application.DTOs;

namespace UserProfileService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DayResultController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DayResultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetDayResultByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("by-period")]
        public async Task<IActionResult> GetByPeriod(int profileId, DateOnly startDate, DateOnly endDate)
        {
            var result = await _mediator.Send(new GetDayResultsByPeriodQuery(profileId, startDate, endDate));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDayResultDTO dto)
        {
            var result = await _mediator.Send(new CreateDayResultCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDayResultDTO dto)
        {
            var result = await _mediator.Send(new UpdateDayResultCommand(dto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteDayResultCommand(id));
            return NoContent();
        }

        [HttpGet("or-create")]
        public async Task<IActionResult> GetOrCreate(int profileId, DateOnly date)
        {
            var result = await _mediator.Send(new GetOrCreateDayResultCommand(profileId, date));
            return Ok(result);
        }
    }
}
