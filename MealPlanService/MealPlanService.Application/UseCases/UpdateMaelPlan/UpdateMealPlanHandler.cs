using AutoMapper;
using MediatR;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Application.Exceptions;
using MealPlanService.Domain.Entities;

namespace MealPlanService.Application.UseCases
{
    public class UpdateMealPlanHandler : IRequestHandler<UpdateMealPlanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMealPlanHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateMealPlanCommand request, CancellationToken cancellationToken)
        {
            var existingMealPlan = await _unitOfWork.MealPlans.GetByIdAsync(request.MealPlan.Id);

            if (existingMealPlan == null)
            {
                throw new NotFoundException("Meal plan not found");
            }

            if (request.userId != existingMealPlan.OwnerId)
                throw new UnauthorizedException("Owner isn't valid");

            _mapper.Map(request.MealPlan, existingMealPlan);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
