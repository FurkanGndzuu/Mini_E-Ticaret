using ETicaretAPI.Application.Features.Commands.Endpoint;
using ETicaretAPI.Application.Features.Queries.Endpoint;
using ETicaretAPI.Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public EndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AssigRoleToEndpoints([FromBody]AssignRoleToEndpointCommandRequest request)
        {
            request.type = typeof(Program);
            AssignRoleToEndpointCommandResponse response =await _mediator.Send(request);
            return Ok();
        }

        [HttpGet]

        public async Task<IActionResult> GetEndPointsRole([FromQuery]GetRolesToEndpointQueryRequest request)
        {
            GetRolesToEndpointQueryResponse response = await _mediator.Send(request);
            return Ok(response.list);
        }
    }
}
