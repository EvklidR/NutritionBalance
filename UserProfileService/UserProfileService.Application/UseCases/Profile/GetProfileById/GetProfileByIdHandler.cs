using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class GetProfileByIdHandler : IRequestHandler<GetProfileByIdQuery, Domain.Entities.Profile>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProfileByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Profile> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.profileId);
            if (profile == null)
                throw new NotFoundException("profile not found");

            if (profile.UserId != request.userId)
                throw new UnauthorizedException("Owner isn't valid");
            return profile;
        }
    }
}
