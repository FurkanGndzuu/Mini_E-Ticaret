﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class CompletedOrderCommandRequest : IRequest<CompletedOrderCommandResponse>
    {
        public string orderId { get; set; }
    }
}
