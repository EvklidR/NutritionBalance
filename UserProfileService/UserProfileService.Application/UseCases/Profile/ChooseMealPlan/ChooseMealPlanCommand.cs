using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public record ChooseMealPlanCommand(int MealPlanId, int ProfileId, int userId) : IRequest;
}
