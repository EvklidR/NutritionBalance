﻿using MediatR;

namespace MealPlanService.Application.UseCases
{
    public record DeleteMealPlanCommand(int Id) : IRequest;
}