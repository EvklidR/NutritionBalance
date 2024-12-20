﻿using FluentValidation;

namespace UserProfileService.Application.UseCases.Dish
{
    public class GetAllDishesQueryValidator : AbstractValidator <GetAllDishesQuery>
    {
        public GetAllDishesQueryValidator() 
        {
            RuleFor(x => x.ProfileId).GreaterThan(0).WithMessage("Profile id must be greater than 0");
        }
    }
}
