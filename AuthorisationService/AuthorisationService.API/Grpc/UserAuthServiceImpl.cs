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

    public override async Task<MakeUserAdminResponse> MakeUserAdmin(MakeUserAdminRequest request, ServerCallContext context)
    {
        try
        {
            await _mediator.Send(new MakeUserAdminCommand(request.UserId));
            return new MakeUserAdminResponse { Success = true};
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error promoting user: {ex.Message}");
            return new MakeUserAdminResponse { Success = false };
        }
    }
}