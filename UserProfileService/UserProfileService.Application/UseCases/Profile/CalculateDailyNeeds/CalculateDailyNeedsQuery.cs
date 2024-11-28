using MediatR;
using UserProfileService.Application.Models;

namespace UserProfileService.Application.UseCases.Profile
{
    public class CalculateDailyNeedsQuery : IRequest<DailyNeedsResponse>
    {
        public int ProfileId { get; set; }
        public CalculateDailyNeedsQuery(int profileId) 
        { 
            ProfileId = profileId;
        }
    }
}
