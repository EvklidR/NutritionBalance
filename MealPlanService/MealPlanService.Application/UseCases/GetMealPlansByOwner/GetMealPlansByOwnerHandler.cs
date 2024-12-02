using MealPlanService.Domain.Entities;
using MediatR;
using MealPlanService.Domain.Interfaces;

namespace MealPlanService.Application.UseCases
{
    public class GetMealPlansByOwnerHandler : IRequestHandler<GetMealPlansByOwnerQuery, IEnumerable<MealPlan>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMealPlansByOwnerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MealPlan>?> Handle(GetMealPlansByOwnerQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.MealPlans.GetByOwnerAsync(request.ownerId);
        }
    }
}
