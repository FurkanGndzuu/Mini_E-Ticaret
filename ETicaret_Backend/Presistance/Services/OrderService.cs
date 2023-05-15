using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repositories.CompletedOrder;
using ETicaretAPI.Application.Repositories.OrderRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderReadRepository _completedOrderReadRepository;
        readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        readonly IMailService _mailService;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository, IMailService mailService = null)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
            _mailService = mailService;
        }



        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Adress = createOrder.Adress,
                Description = createOrder.Description,
                Id = createOrder.BasketId
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<GetAllOrders> GetAllOrdersAsync(int Page, int Size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .Include(o => o.Basket)?
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .ThenInclude(p => p.ProductImageFiles);

            var orders = from order in query?.Skip(Page * Size).Take(Size)
                         join completedOrders in _completedOrderReadRepository.Table
                         on order.Id equals completedOrders.OrderId into co_Order
                         from co in co_Order.DefaultIfEmpty()
                         select new
                         {
                            Id = order.Id,
                            Adress = order.Adress,
                            Description = order.Description,
                            CreatedDate = order.CreatedDate,
                            UserName = order.Basket.User.UserName,
                            Completed = co != null ? true : false
                         };

            var data = query?.Skip(Page *Size).Take(Size);

            return new()
            {
                TotalOrderCount = await query?.CountAsync(),
                Orders = orders?.Select(x => new
                {
                    x.Id,
                    x.Adress,
                    x.Description,
                    x.CreatedDate,
                    x.UserName,
                    x.Completed
                }).ToList()
            };
        }

        public async Task<GetOrderById> GetOrderByIdAsync(string id)
        {
            var orde =  _orderReadRepository?.Table?.Include(o => o.Basket)?
                 .ThenInclude(b => b.User)?
                 .Include(o => o.Basket)?
                 .ThenInclude(b => b.BasketItems)?
                 .ThenInclude(bi => bi.Product)?
                 .ThenInclude(p => p.ProductImageFiles);

            var order =await (from ord in orde
                         join comp in _completedOrderReadRepository.Table
                         on ord.Id equals comp.OrderId into _co
                         from co in _co.DefaultIfEmpty()
                         select new
                         {
                             Id = ord.Id,
                             Adress = ord.Adress,
                             Description = ord.Description,
                             BasketItems = ord.Basket.BasketItems.Select(x => new
                             {
                                 name = x.Product.Name,
                                 price = x.Product.Price,
                                 quantity = x.Quantity,
                                 path = x.Product.ProductImageFiles.FirstOrDefault(pi => pi.showCase == true).path != null ? x.Product.ProductImageFiles.FirstOrDefault(pi => pi.showCase == true).path : null
                             }),
                             UserName = ord.Basket.User.UserName,
                             Completed = co != null ? true : false

                         }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

           

            return new()
            {
                Id = id,
                Adress = order?.Adress,
                Description = order?.Description,
                BasketItems = order.BasketItems,
                UserName = order.UserName,
                Completed = order.Completed
            };
        }

        public async Task CompleteOrderAsync(string orderId)
        {
            var order = await _orderReadRepository.Table.Include(o => o.Basket).ThenInclude(b => b.User).FirstOrDefaultAsync(o => o.Id == Guid.Parse(orderId));
            if(order != null)
            {
                await _completedOrderWriteRepository.AddAsync(new()
                {
                    OrderId = Guid.Parse(orderId)
                });

                await _completedOrderWriteRepository.SaveAsync();

                await _mailService.OrderCompleteMailAsync(order.Basket.User.Email, order.Basket.User.UserName, order.CreatedDate);

               
            }
           
        }
    }
}
