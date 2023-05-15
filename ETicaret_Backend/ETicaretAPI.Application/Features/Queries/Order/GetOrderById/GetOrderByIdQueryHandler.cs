using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
          DTOs.Order.GetOrderById response =  await _orderService.GetOrderByIdAsync(request.Id);
            return new()
            {
                Id = response.Id,
                Adress = response.Adress,
                BasketItems = response.BasketItems,
                Description = response.Description,
                UserName = response.UserName,
                Completed = response.Completed
            };

        }
    }
}
