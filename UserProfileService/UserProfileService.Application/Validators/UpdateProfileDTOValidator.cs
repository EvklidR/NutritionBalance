﻿using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class UpdateProfileDTOValidator : AbstractValidator<UpdateProfileDTO>
    {
        public UpdateProfileDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Height must be greater than 0.");

            RuleFor(x => x.ActivityLevel)
                .IsInEnum().WithMessage("Activity level is invalid.");

            RuleFor(x => x.DesiredGlassesOfWater)
                .GreaterThanOrEqualTo(0).WithMessage("Desired glasses of water must be non-negative.");
        }
    }
}
