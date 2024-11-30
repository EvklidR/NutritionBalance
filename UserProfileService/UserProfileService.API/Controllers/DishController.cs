using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.Application.DTOs;
using UserProfileService.Application.UseCases.Dish;

namespace UserProfileService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishById(int id)
        {
            var result = await _mediator.Send(new GetDishByIdQuery { Id = id });

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetAllDishes(int profileId)
        {
            var result = await _mediator.Send(new GetAllDishesQuery { ProfileId = profileId });

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromForm] CreateDishDTO createDishDTO)
        {
            var result = await _mediator.Send(new CreateDishCommand(createDishDTO));

            return CreatedAtAction(nameof(GetDishById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDish([FromBody] UpdateDishDTO updateDishDTO)
        {
            await _mediator.Send(new UpdateDishCommand(updateDishDTO));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            await _mediator.Send(new DeleteDishCommand(id));

            return NoContent();
        }
    }
}
