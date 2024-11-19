using MediatR;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Enums;
using MealPlanService.Domain.Interfaces;

namespace MealPlanService.Application.UseCases
{
    public class GetMealPlansByTypeQueryHandler : IRequestHandler<GetMealPlansByTypeQuery, IEnumerable<MealPlan>?>
    {
        private readonly IMealPlanRepository _mealPlanRepository;

        public GetMealPlansByTypeQueryHandler(IMealPlanRepository mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }

        public async Task<IEnumerable<MealPlan>?> Handle(GetMealPlansByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _mealPlanRepository.GetByTypeAsync(request.Type, request.PageNumber, request.PageSize);
        }
    }
}
