using AutoMapper;
using MediatR;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Application.Exceptions;

namespace MealPlanService.Application.UseCases
{
    public class UpdateMealPlanCommandHandler : IRequestHandler<UpdateMealPlanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMealPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
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

            _mapper.Map(request.MealPlan, existingMealPlan);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
