using Microsoft.AspNetCore.Mvc;
using MediatR;
using UserProfileService.Application.UseCases.DayResult;
using UserProfileService.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using UserProfileService.API.Filters;

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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var result = await _mediator.Send(new GetDayResultByIdQuery(id, userId));
            return Ok(result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpGet("by-period")]
        public async Task<IActionResult> GetByPeriod(int profileId, DateOnly startDate, DateOnly endDate)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var result = await _mediator.Send(new GetDayResultsByPeriodQuery(profileId, startDate, endDate, userId));
            return Ok(result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDayResultDTO dto)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var result = await _mediator.Send(new CreateDayResultCommand(dto, userId));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDayResultDTO dto)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var result = await _mediator.Send(new UpdateDayResultCommand(dto, userId));
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            await _mediator.Send(new DeleteDayResultCommand(id, userId));
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpGet("get-or-create")]
        public async Task<IActionResult> GetOrCreate(int profileId, DateOnly date)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var result = await _mediator.Send(new GetOrCreateDayResultCommand(profileId, date, userId));
            return Ok(result);
        }
    }
}
