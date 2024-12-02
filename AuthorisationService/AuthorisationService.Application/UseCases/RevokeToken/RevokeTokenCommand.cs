using MediatR;

namespace AuthorisationService.Application.UseCases
{
    public record RevokeTokenCommand(int Id) : IRequest;
}
