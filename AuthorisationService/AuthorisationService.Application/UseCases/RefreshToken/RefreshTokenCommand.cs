using MediatR;
using AuthorisationService.Application.Models;

namespace AuthorisationService.Application.UseCases
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<AuthenticatedResponse>;
}
