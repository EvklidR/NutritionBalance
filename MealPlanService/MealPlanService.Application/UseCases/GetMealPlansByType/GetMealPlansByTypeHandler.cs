using MediatR;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Interfaces;

namespace MealPlanService.Application.UseCases
{
    public class GetMealPlansByTypeHandler : IRequestHandler<GetMealPlansByTypeQuery, IEnumerable<MealPlan>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMealPlansByTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MealPlan>?> Handle(GetMealPlansByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.MealPlans.GetByTypeAsync(request.Type, request.PageNumber, request.PageSize);
        }
    }
}
