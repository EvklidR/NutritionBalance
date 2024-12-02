using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.API.Filters;
using UserProfileService.Application.DTOs;
using UserProfileService.Application.UseCases.Ingredient;

namespace UserProfileService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetIngredients(int profileId)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var ingredients = await _mediator.Send(new GetIngredientsQuery(profileId, userId));
            return Ok(ingredients);
        }

        [Authorize]
        [HttpGet("from-api")]
        public async Task<IActionResult> GetIngredientsFromApi(string name)
        {
            var ingredients = await _mediator.Send(new GetProductsFromAPIQuery(name));
            return Ok(ingredients);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDTO createIngredientDTO)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var ingredient = await _mediator.Send(new CreateIngredientCommand(createIngredientDTO, userId));
            return CreatedAtAction(nameof(GetIngredients), new { profileId = ingredient.ProfileId }, ingredient);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPut]
        public async Task<IActionResult> UpdateIngredient([FromBody] UpdateIngredientDTO updateIngredientDTO)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            await _mediator.Send(new UpdateIngredientCommand(updateIngredientDTO, userId));
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            await _mediator.Send(new DeleteIngredientCommand(ingredientId, userId));
            return NoContent();
        }
    }
}
