using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.API.Filters;
using UserProfileService.Application.DTOs;
using UserProfileService.Application.UseCases.Meal;

namespace UserProfileService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MealController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MealController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPost]
        public async Task<IActionResult> AddMeal([FromBody] CreateMealDTO createMealDTO)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var command = new CreateMealCommand(createMealDTO, userId);
            await _mediator.Send(command);
            return CreatedAtAction(nameof(AddMeal), null);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpDelete("{mealId}/day/{dayId}")]
        public async Task<IActionResult> DeleteMeal(int mealId, int dayId)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var command = new DeleteMealCommand(mealId, dayId, userId);
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPut]
        public async Task<IActionResult> UpdateMeal([FromBody] UpdateMealDTO updateMealDTO)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var command = new UpdateMealCommand(updateMealDTO, userId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
