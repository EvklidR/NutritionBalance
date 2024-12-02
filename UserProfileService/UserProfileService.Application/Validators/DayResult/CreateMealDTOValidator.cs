using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class CreateMealDTOValidator : AbstractValidator<CreateMealDTO>
    {
        public CreateMealDTOValidator(CreateEatenFoodDTOValidator сreateEatenFoodDTOValidator)
        {
            RuleFor(x => x.DayResultId)
                .GreaterThan(0).WithMessage("Day id must be grater than 0");
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Meal name is required.");

            RuleForEach(m => m.Foods).SetValidator(сreateEatenFoodDTOValidator);
        }
    }
}
