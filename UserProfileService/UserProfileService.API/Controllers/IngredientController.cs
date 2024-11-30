using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetIngredients(int profileId)
        {
            var ingredients = await _mediator.Send(new GetIngredientsQuery(profileId));

            if (ingredients == null || !ingredients.Any())
                return NoContent();

            return Ok(ingredients);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDTO createIngredientDTO)
        {
            var ingredient = await _mediator.Send(new CreateIngredientCommand(createIngredientDTO));

            return CreatedAtAction(nameof(GetIngredients), new { profileId = ingredient.ProfileId }, ingredient);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngredient([FromBody] UpdateIngredientDTO updateIngredientDTO)
        {
            await _mediator.Send(new UpdateIngredientCommand(updateIngredientDTO));

            return NoContent();
        }

        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            await _mediator.Send(new DeleteIngredientCommand(ingredientId));

            return NoContent();
        }
    }
}
