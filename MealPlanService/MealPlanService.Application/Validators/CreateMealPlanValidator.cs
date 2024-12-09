using FluentValidation;
using MealPlanService.Application.DTOs;

namespace MealPlanService.Application.Validators
{
    public class CreateMealPlanValidator : AbstractValidator<MealPlanCreateDTO>
    {
        public CreateMealPlanValidator(MealPlanDayValidator mealPlanDayValidator)
        {
            RuleFor(mp => mp.OwnerId)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(mp => mp.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");

            RuleFor(mp => mp.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(mp => mp.Type)
                .IsInEnum().WithMessage("Invalid meal plan type.");

            RuleFor(mp => mp.Days)
                .NotEmpty().WithMessage("At least one day is required.")
                .Must(days => days.Select(d => d.NumberOfDay).Distinct().Count() == days.Count)
                .WithMessage("Days must have unique numbers.")
                .Must(days => days.Select(d => d.NumberOfDay).OrderBy(n => n).SequenceEqual(Enumerable.Range(1, days.Count)))
                .WithMessage("Days must include all numbers from 1 to the total number of days, in any order.");

            RuleForEach(mp => mp.Days).SetValidator(mealPlanDayValidator);
        }
    }
}
