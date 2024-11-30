﻿using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class UpdateDishDTOValidator : AbstractValidator<UpdateDishDTO>
    {
        public UpdateDishDTOValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(d => d.AmountOfPortions)
                .GreaterThan(0);

            RuleForEach(d => d.Ingredients)
                .SetValidator(new IngredientOfDishDTOValidator());
        }
    }
}
