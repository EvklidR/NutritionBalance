using AutoMapper;
using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileDto.Id);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var existingProfiles = await _unitOfWork.ProfileRepository.GetAllAsync(profile.UserId);

            foreach (var prof in existingProfiles)
            {
                if (prof.Name == profile.Name)
                {
                    throw new AlreadyExistsException("Profile with this name in your account already exists");
                }
            }

            _mapper.Map(request.ProfileDto, profile);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
