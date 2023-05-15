using ETicaretAPI.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationServiceController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public ApplicationServiceController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> getAuthorizationEndPoints()
        {
            return Ok(_applicationService.getAuthorizeDefinitionEndPoints(typeof(Program)));
        }
    }
}
