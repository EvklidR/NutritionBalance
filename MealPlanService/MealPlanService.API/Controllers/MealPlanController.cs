using MealPlanService.Application.DTOs;
using MealPlanService.Application.UseCases;
using MealPlanService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class MealPlanController : ControllerBase
{
    private readonly CreateMealPlan _createMealPlanCase;
    private readonly UpdateMealPlan _updateMealPlanCase;

    public MealPlanController(CreateMealPlan createMealPlan, UpdateMealPlan updateMealPlan)
    {
        _createMealPlanCase = createMealPlan;
        _updateMealPlanCase = updateMealPlan;
    }

    [HttpPost("create")]
    [ServiceFilter(typeof(ValidateMealPlanFilter))]
    public async Task<ActionResult> AddMealPlan(MealPlanCreateDTO mealPlanCreateDTO)
    {
        var plan = await _createMealPlanCase.ExecuteAsync(mealPlanCreateDTO);
        return Ok(plan);
    }

    [HttpPost("update")]
    [ServiceFilter(typeof(ValidateMealPlanFilter))]
    public async Task<ActionResult> UpdateMealPlan(MealPlan mealPlan)
    {
        await _updateMealPlanCase.ExecuteAsync(mealPlan);
        return Ok();
    }
}
