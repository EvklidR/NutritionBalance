using MediatR;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class GetIngredientsHandler : IRequestHandler<GetIngredientsQuery, IEnumerable<Domain.Entities.Ingredient>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetIngredientsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Entities.Ingredient>?> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _unitOfWork.IngredientRepository.GetAllAsync(request.ProfileId);
            return dishes;
        }
    }
}
