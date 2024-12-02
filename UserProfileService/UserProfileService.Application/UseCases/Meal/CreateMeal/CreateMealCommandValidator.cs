using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Meal
{
    public class CreateMealCommandValidator : AbstractValidator<CreateMealCommand>
    {
        public CreateMealCommandValidator(CreateMealDTOValidator createMealDTOValidator) 
        {
            RuleFor(x => x.CreateMealDTO)
                .NotEmpty().WithMessage("Create data must be not empty")
                .SetValidator(createMealDTOValidator);
        }
    }
}
