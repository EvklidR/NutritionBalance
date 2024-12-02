﻿using FluentValidation;
using UserProfileService.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace UserProfileService.Application.Validators
{
    public class CreateDishDTOValidator : AbstractValidator<CreateDishDTO>
    {
        public CreateDishDTOValidator(IngredientOfDishDTOValidator ingredientOfDishDTOValidator)
        {
            RuleFor(d => d.ProfileId)
                .GreaterThan(0).WithMessage("ProfileId must be greater than zero.");

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(d => d.AmountOfPortions)
                .GreaterThan(0).WithMessage("Amount should be grater than 0");

            RuleForEach(d => d.Ingredients)
                .SetValidator(ingredientOfDishDTOValidator);
        }
    }
}
