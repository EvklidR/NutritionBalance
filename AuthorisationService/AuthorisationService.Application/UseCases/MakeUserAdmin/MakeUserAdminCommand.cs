using MediatR;

namespace AuthorisationService.Application.UseCases
{
    public record MakeUserAdminCommand(int userId) : IRequest;
}
