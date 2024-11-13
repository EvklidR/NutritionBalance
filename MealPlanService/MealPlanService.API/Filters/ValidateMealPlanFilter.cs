using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MealPlanService.Domain.Entities;
using AutoMapper;
using MealPlanService.Application.DTOs;

public class ValidateMealPlanFilter : IAsyncActionFilter
{
    private readonly IValidator<MealPlan> _mealPlanValidator;
    private readonly IMapper _mapper;

    public ValidateMealPlanFilter(IValidator<MealPlan> mealPlanValidator, IMapper mapper)
    {
        _mealPlanValidator = mealPlanValidator;
        _mapper = mapper;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var model = context.ActionArguments.Values.OfType<object>().FirstOrDefault();

        if (model != null)
        {
            if (model is MealPlanCreateDTO mealPlanCreateDTO)
            {
                MealPlan mealPlan = _mapper.Map<MealPlan>(mealPlanCreateDTO);
                var result = _mealPlanValidator.Validate(mealPlan);

                if (!result.IsValid)
                {
                    context.Result = new BadRequestObjectResult(result.Errors);
                    return;
                }
            }
            else if (model is MealPlan mealPlan)
            {
                var result = _mealPlanValidator.Validate(mealPlan);

                if (!result.IsValid)
                {
                    context.Result = new BadRequestObjectResult(result.Errors);
                    return;
                }
            }
        }

        await next();
    }
}
