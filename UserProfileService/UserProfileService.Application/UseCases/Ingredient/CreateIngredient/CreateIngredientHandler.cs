using AutoMapper;
using MediatR;
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
            var ingredient = _mapper.Map<Domain.Entities.Ingredient>(request.IngredientDTO);

            _unitOfWork.IngredientRepository.Add(ingredient);
            await _unitOfWork.SaveChangesAsync();

            return ingredient;
        }
    }
}
