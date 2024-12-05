using FluentValidation;

namespace AuthorisationService.Application.UseCases
{
    public class MakeUserAdminCommandValidator : AbstractValidator<MakeUserAdminCommand>
    {
        public MakeUserAdminCommandValidator() 
        {
            RuleFor(x => x.userId)
                .GreaterThan(0).WithMessage("User is must be grater than 0");
        }
    }
}