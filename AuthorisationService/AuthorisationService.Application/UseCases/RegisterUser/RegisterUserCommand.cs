using MediatR;
using AuthorisationService.Application.Models;
using AuthorisationService.Application.DTOs;

namespace AuthorisationService.Application.UseCases
{
    public record RegisterUserCommand(CreateUserDto CreateUserDto) : IRequest<AuthenticatedResponse>;
}
