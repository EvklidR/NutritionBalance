using AutoMapper;
using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Enums;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class AddProfileHandler : IRequestHandler<AddProfileCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = _mapper.Map<Domain.Entities.Profile>(request.ProfileDto);

            var existingProfiles = await _unitOfWork.ProfileRepository.GetAllAsync(profile.UserId);

            foreach (var prof in existingProfiles)
            {
                if (prof.Name == profile.Name)
                {
                    throw new AlreadyExistsException("Profile with this name already exists");
                }
            }
            profile.DesiredGlassesOfWater = profile.Gender == Gender.Female ? 11 : 15;
            _unitOfWork.ProfileRepository.Add(profile);
            await _unitOfWork.SaveChangesAsync();
            return profile.Id;
        }
    }
}
