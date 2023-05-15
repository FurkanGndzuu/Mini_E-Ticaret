using ETicaretAPI.Application.Features.Commands.Users.FacebookLogin;
using ETicaretAPI.Application.Features.Commands.Users.GoogleLoginUser;
using ETicaretAPI.Application.Features.Commands.Users.LoginUser;
using ETicaretAPI.Application.Features.Commands.Users.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        readonly IMediator _mediator;
        public AuthsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginUserCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginUserCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FcaebookLoginCommandRequest request)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("refresh-token-login")]

        public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginCommandRequest request)
        {
          RrefreshTokenLoginCommandResponse response =  await _mediator.Send(request);
            return Ok(response);
        }
    }
}
