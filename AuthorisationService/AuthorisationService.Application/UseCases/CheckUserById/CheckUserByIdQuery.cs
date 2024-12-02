using MediatR;

namespace AuthorisationService.Application.UseCases
{
    public record CheckUserByIdQuery(int Id) : IRequest<bool>;
}
