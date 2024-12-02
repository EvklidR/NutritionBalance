using AutoMapper;
using MediatR;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Application.UseCases.Meal
{
    public class CreateMealHandler : IRequestHandler<CreateMealCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMealHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            var day = await _unitOfWork.DayResultRepository.GetByIdAsync(request.CreateMealDTO.DayResultId);
            if (day == null) 
            {
                throw new NotFoundException("Day not found");
            }

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(day.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var createMeal = _mapper.Map<Domain.Entities.Meal>(request.CreateMealDTO);
            day.Meals.Add(createMeal);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
