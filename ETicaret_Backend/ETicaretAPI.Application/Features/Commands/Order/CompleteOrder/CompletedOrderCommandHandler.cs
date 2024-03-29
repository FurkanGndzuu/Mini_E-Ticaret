﻿using ETicaretAPI.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class CompletedOrderCommandHandler : IRequestHandler<CompletedOrderCommandRequest, CompletedOrderCommandResponse>
    {
        readonly IOrderService _orderService;

        public CompletedOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CompleteOrderAsync(request.orderId);
            return new();
        }
    }
}
