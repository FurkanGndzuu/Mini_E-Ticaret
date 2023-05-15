using F = ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.DTOs.Order;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IBasketService _basketService;
        readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
           F.Basket? basket = _basketService.Basket;

            DTOs.Order.CreateOrder createOrder = new() {
            
                BasketId = basket.Id,
                Adress = request.Adress,
                Description = request.Description

            };

            await _orderService.CreateOrderAsync(createOrder);

            return new();
        }
    }
}
