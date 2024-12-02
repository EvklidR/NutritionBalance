using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.API.Filters;
using UserProfileService.Application.DTOs;
using UserProfileService.Application.UseCases.Profile;

namespace UserProfileService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDTO profileDto)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            profileDto.UserId = userId;
            var command = new CreateProfileCommand(profileDto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(null, null, result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpGet("{profileId}/daily-needs")]
        public async Task<IActionResult> CalculateDailyNeeds(int profileId)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var query = new CalculateDailyNeedsQuery(profileId, userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPatch("choose-meal-plan")]
        public async Task<IActionResult> ChooseMealPlan(int profileId, int mealPlanId)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var command = new ChooseMealPlanCommand(mealPlanId, profileId, userId);
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteProfile(int profileId)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var command = new DeleteProfileCommand(profileId, userId);
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpGet("by-user")]
        public async Task<IActionResult> GetUserProfiles()
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var query = new GetUserProfilesQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPatch("{profileId}/revoke-meal-plan")]
        public async Task<IActionResult> RevokeMealPlan(int profileId)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var command = new RevokeMealPlanCommand(profileId, userId);
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPut("{profileId}")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO profileDto)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var command = new UpdateProfileCommand(profileDto, userId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
