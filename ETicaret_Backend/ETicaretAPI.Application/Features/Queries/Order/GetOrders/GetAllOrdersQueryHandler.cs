using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Order.GetOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
          GetAllOrders orders =  await _orderService.GetAllOrdersAsync(request.Page, request.Size);
            return new()
            {
                TotalOrderCount = orders.TotalOrderCount,
                Orders = orders.Orders
            };
        }
    }
}
