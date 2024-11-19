using AutoMapper;
using MediatR;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Interfaces;

namespace MealPlanService.Application.UseCases
{
    public class CreateMealPlanCommandHandler : IRequestHandler<CreateMealPlanCommand, MealPlan>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMealPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MealPlan> Handle(CreateMealPlanCommand request, CancellationToken cancellationToken)
        {
            var mealPlan = _mapper.Map<MealPlan>(request.MealPlanDto);

            _unitOfWork.MealPlans.Add(mealPlan);
            await _unitOfWork.SaveChangesAsync();

            return mealPlan;
        }
    }
}
