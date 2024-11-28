using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfileService.Application.UseCases.Profile
{
    internal class CalculateDailyNeedsQueryValidator : AbstractValidator<CalculateDailyNeedsQuery>
    {
        public CalculateDailyNeedsQueryValidator() 
        {
            RuleFor(x => x.ProfileId).GreaterThan(0);
        }
    }
}
