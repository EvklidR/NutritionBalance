using MediatR;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Profile
{
    public class DeleteProfileHandler : IRequestHandler<DeleteProfileCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileId);

            if (profile == null)
                throw new KeyNotFoundException("Profile not found.");

            _unitOfWork.ProfileRepository.Delete(profile);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
