using AutoMapper;
using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Application.Interfaces;
using UserProfileService.Domain.Enums;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class CreateProfileHandler : IRequestHandler<CreateProfileCommand, Domain.Entities.Profile>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public CreateProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Domain.Entities.Profile> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = _mapper.Map<Domain.Entities.Profile>(request.ProfileDto);

            var userExists = await _userService.CheckUserByIdAsync(profile.UserId);
            if (!userExists)
            {
                throw new UnauthorizedException("User does not exist");
            }

            var existingProfiles = await _unitOfWork.ProfileRepository.GetAllAsync(profile.UserId);

            foreach (var prof in existingProfiles)
            {
                if (prof.Name == profile.Name)
                {
                    throw new AlreadyExistsException("Profile with this name in your account already exists");
                }
            }

            profile.DesiredGlassesOfWater = profile.Gender == Gender.Female ? 11 : 15;
            _unitOfWork.ProfileRepository.Add(profile);
            await _unitOfWork.SaveChangesAsync();
            return profile;
        }
    }
}
