using ETicaretAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions
{
    public  interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);

        Task<GetAllOrders> GetAllOrdersAsync(int Page ,int Size);
        Task<GetOrderById> GetOrderByIdAsync(string id);

        Task CompleteOrderAsync(string orderId);
    }
}
