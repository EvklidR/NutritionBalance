using MealPlanService.Application.Exceptions;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Interfaces;
using MediatR;

namespace MealPlanService.Application.UseCases
{
    public class GetMealPlanByIdHandler : IRequestHandler<GetMealPlanByIdQuery, MealPlan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMealPlanByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MealPlan> Handle(GetMealPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var mealPlan = await _unitOfWork.MealPlans.GetByIdAsync(request.mealPlanId);

            if (mealPlan == null)
                throw new NotFoundException("Meal plan not found");

            return mealPlan;
        }
    }
}
