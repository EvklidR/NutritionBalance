using MediatR;
using AuthorisationService.Application.Models;

namespace AuthorisationService.Application.UseCases
{
    public record LoginUserCommand(string Username, string Password) : IRequest<AuthenticatedResponse>;
}
