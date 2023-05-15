using ETicaretAPI.Application.Features.Commands.ProductImageFile.ChooseImageFile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        readonly IConfiguration _configuration;
        readonly IMediator _mediator;

        public FilesController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChooseImageFile([FromBody]ChooseImageFileCommandRequest request)
        {
           ChooseImageFileCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
