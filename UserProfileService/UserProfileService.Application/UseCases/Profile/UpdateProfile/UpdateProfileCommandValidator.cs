﻿using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Profile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator(UpdateProfileDTOValidator updateValidator)
        {
            RuleFor(x => x.ProfileDto)
                .NotNull()
                .WithMessage("Profile data is required.")
                .SetValidator(updateValidator);
        }
    }
}