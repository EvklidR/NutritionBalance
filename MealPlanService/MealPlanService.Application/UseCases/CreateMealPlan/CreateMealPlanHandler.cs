using AutoMapper;
using MediatR;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Application.Interfaces;
using MealPlanService.Application.Exceptions;

namespace MealPlanService.Application.UseCases
{
    public class CreateMealPlanHandler : IRequestHandler<CreateMealPlanCommand, MealPlan>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CreateMealPlanHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<MealPlan> Handle(CreateMealPlanCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userService.CheckUserByIdAsync(request.MealPlanDto.OwnerId);
            if (!userExists)
            {
                throw new UnauthorizedException("User does not exist");
            }

            var mealPlan = _mapper.Map<MealPlan>(request.MealPlanDto);

            _unitOfWork.MealPlans.Add(mealPlan);
            await _unitOfWork.SaveChangesAsync();

            return mealPlan;
        }
    }
}
