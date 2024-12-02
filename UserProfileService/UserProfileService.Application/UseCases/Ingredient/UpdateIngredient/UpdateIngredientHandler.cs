using AutoMapper;
using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Ingredient.UpdateIngredient
{
    public class UpdateIngredientHandler : IRequestHandler<UpdateIngredientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateIngredientHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(request.IngredientDTO.Id);

            if (ingredient == null)
                throw new NotFoundException("Ingredient not found");

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(ingredient.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            _mapper.Map(request.IngredientDTO, ingredient);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
