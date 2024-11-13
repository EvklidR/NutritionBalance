using AutoMapper;
using MealPlanService.Application.DTOs;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Interfaces;

namespace MealPlanService.Application.UseCases
{
    public class CreateMealPlan
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMealPlan(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MealPlan> ExecuteAsync(MealPlanCreateDTO mealPlanDto)
        {
            var mealPlan = _mapper.Map<MealPlan>(mealPlanDto);

            _unitOfWork.MealPlans.Add(mealPlan);

            await _unitOfWork.SaveChangesAsync();

            return mealPlan;
        }
    }
}
