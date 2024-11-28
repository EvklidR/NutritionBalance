using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class CreateEatenFoodDTOValidator : AbstractValidator<CreateEatenFoodDTO>
    {
        public CreateEatenFoodDTOValidator()
        {
            RuleFor(f => f.FoodId)
                .GreaterThan(0).WithMessage("FoodId must be greater than 0.");

            RuleFor(f => f.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");
        }
    }
}
