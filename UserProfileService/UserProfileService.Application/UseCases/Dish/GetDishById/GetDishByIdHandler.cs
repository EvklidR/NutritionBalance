using MediatR;
using AutoMapper;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

namespace UserProfileService.Application.UseCases.Dish
{
    public class GetDishByIdHandler : IRequestHandler<GetDishByIdQuery, Domain.Entities.Dish>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDishByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Dish> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(request.Id);
            if (dish == null)
                throw new NotFoundException($"Dish with ID {request.Id} not found.");

            return dish;
        }
    }
}
