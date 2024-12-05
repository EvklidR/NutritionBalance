using AuthorisationService.Application.Interfaces;
using AuthorisationService.Domain.Interfaces;
using MediatR;

namespace AuthorisationService.Application.UseCases
{
    public class MakeUserAdminHandler : IRequestHandler<MakeUserAdminCommand>
    {
        private readonly IUserRepository _userRepository;

        public MakeUserAdminHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(MakeUserAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId);
        }
    }
}
