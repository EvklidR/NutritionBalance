using MediatR;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

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

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileId);

            if (profile == null) 
            {
                throw new NotFoundException("Profile not found");
            }

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var dishes = await _unitOfWork.IngredientRepository.GetAllAsync(request.ProfileId);
            return dishes;
        }
    }
}
