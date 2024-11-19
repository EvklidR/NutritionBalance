using MediatR;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Application.Exceptions;

namespace MealPlanService.Application.UseCases
{
    public class DeleteMealPlanCommandHandler : IRequestHandler<DeleteMealPlanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMealPlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteMealPlanCommand request, CancellationToken cancellationToken)
        {
            var mealPlan = await _unitOfWork.MealPlans.GetByIdAsync(request.Id);

            if (mealPlan == null)
            {
                throw new NotFoundException("Meal plan with this id not found");
            }

            _unitOfWork.MealPlans.Delete(mealPlan);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
