using AutoMapper;
using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class CreateIngredientHandler : IRequestHandler<CreateIngredientCommand, Domain.Entities.Ingredient>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateIngredientHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Ingredient> Handle(
            CreateIngredientCommand request,
            CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.IngredientDTO.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var ingredient = _mapper.Map<Domain.Entities.Ingredient>(request.IngredientDTO);

            _unitOfWork.IngredientRepository.Add(ingredient);
            await _unitOfWork.SaveChangesAsync();

            return ingredient;
        }
    }
}
