using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public class RevokeMealPlanCommand : IRequest
    {
        public int ProfileId { get; set; }
        public RevokeMealPlanCommand(int profileId) 
        {
            ProfileId = profileId;
        }
    }
}
