using MealPlanService.Application.UseCases;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class MealPlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public MealPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [ServiceFilter(typeof(ValidateMealPlanFilter))]
    public async Task<ActionResult> AddMealPlan(CreateMealPlanCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("update")]
    [ServiceFilter(typeof(ValidateMealPlanFilter))]
    public async Task<ActionResult> UpdateMealPlan(UpdateMealPlanCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("delete")]
    [ServiceFilter(typeof(ValidateMealPlanFilter))]
    public async Task<ActionResult> DeleteMealPlan(DeleteMealPlanCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("category")]
    public async Task<ActionResult<IEnumerable<MealPlan>>> GetMealPlansByCategory(MealPlanType type, int pageNumber = 1, int pageSize = 3)
    {
        var query = new GetMealPlansByTypeQuery(type, pageNumber, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("calculate-kcal")]
    public async Task<ActionResult> CalculateKcalAndMacros([FromBody] GetKcalAndMacrosQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
