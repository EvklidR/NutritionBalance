using Microsoft.AspNetCore.Mvc;
using MediatR;
using AuthorisationService.Application.UseCases;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using AuthorisationService.API.Filters;

namespace AuthorisationService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        //[EnableCors("AllowJavaOrigin")]
        //[Authorize]
        //[ServiceFilter(typeof(UserIdFilter))]
        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeRole()
        {
           // int id = (int)HttpContext.Items["UserId"]!;
            await _mediator.Send(new MakeUserAdminCommand(1));
            return Ok();
        }
    }
}
