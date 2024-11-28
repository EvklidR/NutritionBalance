using AutoMapper;
using MediatR;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class GetUserProfilesHandler : IRequestHandler<GetUserProfilesQuery, IEnumerable<Domain.Entities.Profile>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserProfilesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain.Entities.Profile>?> Handle(
            GetUserProfilesQuery request,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProfileRepository.GetAllAsync(request.UserId);
        }
    }
}
