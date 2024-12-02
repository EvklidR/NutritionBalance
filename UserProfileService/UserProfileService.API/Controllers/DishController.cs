using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.API.Filters;
using UserProfileService.Application.DTOs;
using UserProfileService.Application.Interfaces;
using UserProfileService.Application.UseCases.Dish;

namespace UserProfileService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IImageService _imageService;

        public DishController(IMediator mediator, IImageService imageService)
        {
            _mediator = mediator;
            _imageService = imageService;
        }

        [Authorize]
        [HttpGet("get-file/{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            byte[] fileBytes = await _imageService.GetCashedImageAsync(fileName);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPatch("update-file")]
        public async Task<IActionResult> UpdateFile([FromForm] int dishId, IFormFile file)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var coomand = new UpdateImageCommand(file, dishId, userId);
            await _mediator.Send(coomand);
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishById(int id)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var query = new GetDishByIdQuery(id, userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetAllDishes(int profileId)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var query = new GetAllDishesQuery(profileId, userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPost]
        public async Task<IActionResult> CreateDish(CreateDishDTO createDishDTO)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            var result = await _mediator.Send(new CreateDishCommand(createDishDTO, userId));
            return CreatedAtAction(nameof(GetDishById), new { id = result.Id }, result);
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpPut]
        public async Task<IActionResult> UpdateDish(UpdateDishDTO updateDishDTO)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            await _mediator.Send(new UpdateDishCommand(updateDishDTO, userId));
            return NoContent();
        }

        [Authorize]
        [ServiceFilter(typeof(UserIdFilter))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var userId = (int)HttpContext.Items["UserId"]!;
            await _mediator.Send(new DeleteDishCommand(id, userId));
            return NoContent();
        }
    }
}
