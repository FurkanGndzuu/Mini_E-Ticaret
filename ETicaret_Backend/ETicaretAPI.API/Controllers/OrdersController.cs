using ETicareAPI.Domain.Enums;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Features.Commands.Order.CompleteOrder;
using ETicaretAPI.Application.Features.Commands.Order.CreateOrder;
using ETicaretAPI.Application.Features.Queries.Order.GetOrderById;
using ETicaretAPI.Application.Features.Queries.Order.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Order", Menu = nameof(MenuTypes.Orders))]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommandRequest request)
        {
           CreateOrderCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Orders", Menu = nameof(MenuTypes.Orders))]
        public async Task<IActionResult> GetAllOrders([FromQuery]GetAllOrdersQueryRequest request)
        {
          GetAllOrdersQueryResponse response =  await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("[action]/{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Order By Id", Menu = nameof(MenuTypes.Orders))]
        public async Task<IActionResult> getOrderById([FromRoute]GetOrderByIdQueryRequest request)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Complete Order", Menu = nameof(MenuTypes.Baskets))]
        public async Task<IActionResult> completeOrder([FromBody]CompletedOrderCommandRequest request)
        {
            CompletedOrderCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
    }
}
