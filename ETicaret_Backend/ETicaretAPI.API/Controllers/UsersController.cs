using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.Configuration;
using ETicaretAPI.Application.Features.Commands.Users.AssignRoles;
using ETicaretAPI.Application.Features.Commands.Users.CreateUser;
using ETicaretAPI.Application.Features.Commands.Users.FacebookLogin;
using ETicaretAPI.Application.Features.Commands.Users.GoogleLoginUser;
using ETicaretAPI.Application.Features.Commands.Users.LoginUser;
using ETicaretAPI.Application.Features.Queries.User.GetAllUsers;
using ETicaretAPI.Application.Features.Queries.User.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly UserManager<AppUser> _userManager;
        readonly IMediator _mediator;
        readonly IApplicationService _applicationService;


        public UsersController(UserManager<AppUser> userManager, IMediator mediator, IApplicationService applicationService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _applicationService = applicationService;
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] CreateUserCommandRequest request)
        {
           CreateUserCommandResponse response  = await _mediator.Send(request);
            return Ok(response);    
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ETicareAPI.Domain.Enums.ActionType.Reading , Definition = "Get All User" , Menu =nameof(MenuTypes.Users))]
        public async Task<IActionResult> GetUsers([FromQuery]GetAllUsersQueryRequest request)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("assign-roles-to-user")]
        [AuthorizeDefinition(ActionType = ETicareAPI.Domain.Enums.ActionType.Writing, Definition = "Assign Roles To User", Menu = nameof(MenuTypes.Users))]

        public async Task<IActionResult> AssignRolesToUser([FromBody] AssignRolesToUserCommandRequest request)
        {
            AssignRolesToUserCommandResponse response = await _mediator.Send(request);
            
            return Ok();
        }

        [HttpGet("get-roles-to-user")]
        [AuthorizeDefinition(ActionType = ETicareAPI.Domain.Enums.ActionType.Reading, Definition = "Get Roles To User", Menu = nameof(MenuTypes.Users))]

        public async Task<IActionResult> GetRolesToUser([FromQuery] GetRolesToUserQueryRequest request)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(request);

            return Ok(response.list);
        }

    }
}
