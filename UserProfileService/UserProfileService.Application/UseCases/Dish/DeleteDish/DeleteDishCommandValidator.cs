using FluentValidation;

namespace UserProfileService.Application.UseCases.Dish
{
    public class DeleteDishCommandValidator : AbstractValidator<DeleteDishCommand>
    {
        public DeleteDishCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Dish ID must be greater than zero.");
        }
    }
}
