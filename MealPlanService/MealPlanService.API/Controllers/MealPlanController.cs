using MealPlanService.API.Filters;
using MealPlanService.Application.DTOs;
using MealPlanService.Application.Interfaces;
using MealPlanService.Application.UseCases;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class MealPlanController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IImageService _imageService;

    public MealPlanController(IMediator mediator, IImageService imageService)
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

    [Authorize(Roles="admin")]
    [ServiceFilter(typeof(UserIdFilter))]
    [HttpPatch("update-file")]
    public async Task<IActionResult> UpdateFile([FromForm] int mealPlanId, IFormFile file)
    {
        var userId = (int)HttpContext.Items["UserId"]!;
        var coomand = new UpdateImageCommand(file, mealPlanId, userId);
        await _mediator.Send(coomand);
        return NoContent();
    }

    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(UserIdFilter))]
    [HttpPost("create")]
    public async Task<ActionResult> AddMealPlan(CreateMealPlanCommand command)
    {
        command.MealPlanDto.OwnerId = (int)HttpContext.Items["UserId"]!;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(UserIdFilter))]
    [HttpPut("update")]
    public async Task<ActionResult> UpdateMealPlan(UpdateMealPlanDTO updateMealPlanDTO)
    {
        var userId = (int)HttpContext.Items["UserId"]!;
        var command = new UpdateMealPlanCommand(updateMealPlanDTO, userId);
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(UserIdFilter))]
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteMealPlan(int id)
    {
        var userId = (int)HttpContext.Items["UserId"]!;
        var command = new DeleteMealPlanCommand(id, userId);
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpGet("by-category")]
    public async Task<ActionResult<IEnumerable<MealPlan>?>> GetMealPlansByCategory(MealPlanType type, int pageNumber = 1, int pageSize = 3)
    {
        var query = new GetMealPlansByTypeQuery(type, pageNumber, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(UserIdFilter))]
    [HttpGet("by-owner")]
    public async Task<ActionResult<IEnumerable<MealPlan>?>> GetMealPlansByOwner()
    {
        var userId = (int)HttpContext.Items["UserId"]!;
        var query = new GetMealPlansByOwnerQuery(userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("calculate-kcal")]
    public async Task<ActionResult> CalculateKcalAndMacros([FromQuery] GetKcalAndMacrosQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
