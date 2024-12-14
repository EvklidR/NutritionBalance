using MediatR;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class GetUserProfilesHandler : IRequestHandler<GetUserProfilesQuery, IEnumerable<Domain.Entities.Profile>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserProfilesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Entities.Profile>?> Handle(
            GetUserProfilesQuery request,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProfileRepository.GetAllAsync(request.UserId);
        }
    }
}
