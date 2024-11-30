
using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public class ChooseMealPlanCommand : IRequest
    {
        public int MealPlanId { get; set; }
        public int ProfileId { get; set; }

        public ChooseMealPlanCommand (int mealPlanId, int profileId)
        {
            MealPlanId = mealPlanId;
            ProfileId = profileId;
        }   
    }
}
