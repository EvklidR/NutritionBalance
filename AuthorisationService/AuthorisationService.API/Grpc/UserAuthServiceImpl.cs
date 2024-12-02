using Grpc.Core;
using AuthorisationService.Grpc;
using AuthorisationService.Application.UseCases;
using MediatR;

public class UserAuthServiceImpl : UserAuthService.UserAuthServiceBase
{
    private readonly IMediator _mediator;

    public UserAuthServiceImpl(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CheckUserByIdResponse> CheckUserById(CheckUserByIdRequest request, ServerCallContext context)
    {
        var exists = await _mediator.Send(new CheckUserByIdQuery(request.Id));

        return new CheckUserByIdResponse { Exists = exists };
    }
}
