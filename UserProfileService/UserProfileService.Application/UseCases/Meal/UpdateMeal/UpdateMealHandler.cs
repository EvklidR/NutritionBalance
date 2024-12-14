using AutoMapper;
using MediatR;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

namespace UserProfileService.Application.UseCases.Meal
{
    public class UpdateMealHandler : IRequestHandler<UpdateMealCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMealHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateMealCommand request, CancellationToken cancellationToken)
        {
            var day = await _unitOfWork.DayResultRepository.GetByIdAsync(request.UpdateMealDTO.DayResultId);
            if (day == null)
            {
                throw new NotFoundException("Day not found");
            }

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(day.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var meal = day.Meals.FirstOrDefault(m => m.Id == request.UpdateMealDTO.Id);
            if (meal == null)
            {
                throw new NotFoundException("Meal not found");
            }
        }
    }
}
