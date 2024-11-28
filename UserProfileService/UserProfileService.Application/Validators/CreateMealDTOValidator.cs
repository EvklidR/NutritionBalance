using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class CreateMealDTOValidator : AbstractValidator<CreateMealDTO>
    {
        public CreateMealDTOValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Meal name is required.");

            RuleForEach(m => m.Foods).SetValidator(new CreateEatenFoodDTOValidator());
        }
    }
}
