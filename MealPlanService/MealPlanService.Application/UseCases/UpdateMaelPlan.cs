using AutoMapper;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Application.Exceptions;

namespace MealPlanService.Application.UseCases
{
    public class UpdateMealPlan
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMealPlan(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(MealPlan mealPlan)
        {
            var executingMealPlan = await _unitOfWork.MealPlans.GetByIdAsync(mealPlan.Id);

            if (executingMealPlan == null)
            {
                throw new NotFoundException("Meal plan not found");
            }

            _mapper.Map(mealPlan, executingMealPlan);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
