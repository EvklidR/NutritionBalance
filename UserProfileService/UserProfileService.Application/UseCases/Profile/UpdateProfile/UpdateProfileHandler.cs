using AutoMapper;
using MediatR;
using UserProfileService.Application.DTOs;
using UserProfileService.Application.Exceptions;
using UserProfileService.Application.UseCases.DayResult;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
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
                if ((prof.Name == profile.Name) && (prof != profile))
                {
                    throw new AlreadyExistsException("Profile with this name in your account already exists");
                }
            }

            _mapper.Map(request.ProfileDto, profile);

            if (request.ProfileDto.Weight!= null)
            {
                var dayResult = await _mediator.Send(
                    new GetOrCreateDayResultCommand(
                        profile.Id,
                        DateOnly.FromDateTime(DateTime.Now),
                        profile.UserId));

                dayResult.Weight = request.ProfileDto.Weight;

            }

            await _unitOfWork.SaveChangesAsync();
        }

        public double? GetActivityMultiplier(ActivityLevel? activityLevel)
        {
            return activityLevel switch
            {
                ActivityLevel.sedentary => 1.2,
                ActivityLevel.low => 1.375,
                ActivityLevel.medium => 1.55,
                ActivityLevel.high => 1.725,
                ActivityLevel.veryHigh => 1.9,
                null => null
            };
        }
    }
}
