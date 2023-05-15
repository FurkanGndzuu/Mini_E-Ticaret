using ETicareAPI.Domain.Enums;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Features.Commands.Basket;
using ETicaretAPI.Application.Features.Commands.Basket.DeleteBasketItem;
using ETicaretAPI.Application.Features.Commands.Basket.UpdateBasketItem;
using ETicaretAPI.Application.Features.Queries.Basket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="admin")]
    public class BasketsController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing , Definition ="Add Basket Item" , Menu = nameof(MenuTypes.Baskets))]
        public async Task<IActionResult> Add(AddBasketItemCommandRequest request)
        {
            AddBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Basket Items", Menu = nameof(MenuTypes.Baskets))]
        public async Task<IActionResult> Get()
        {
            ListBasketItemQueryRequest request = new();
            List<ListBasketItemQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{basketItemId}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Basket Item", Menu = nameof(MenuTypes.Baskets))]
        public async Task<IActionResult> delete([FromRoute]DeleteBasketItemCommandRequest request)
        {
          DeleteBasketItemCommandResponse response =  await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Basket Item", Menu = nameof(MenuTypes.Baskets))]
        public async Task<IActionResult> update([FromBody]UpdateBasketItemCommandRequest request)
        {
           UpdateBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
    }
}
