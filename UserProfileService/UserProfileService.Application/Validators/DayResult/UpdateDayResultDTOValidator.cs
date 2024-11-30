using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class UpdateDayResultDTOValidator : AbstractValidator<UpdateDayResultDTO>
    {
        public UpdateDayResultDTOValidator()
        {
            RuleForEach(dr => dr.Meals).SetValidator(new CreateMealDTOValidator());
        }
    }
}
