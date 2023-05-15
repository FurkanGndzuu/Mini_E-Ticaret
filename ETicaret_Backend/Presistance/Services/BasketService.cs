using ETicareAPI.Domain.Entities;
using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Application.Repositories.BasketItem;
using ETicaretAPI.Application.Repositories.OrderRepositories;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using ETicaretAPI.Application.ViewModels.Baskets_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Services
{
    public class BasketService : IBasketService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly IBasketItemReadRepository _basketItemReadRepository;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;
        readonly IBasketReadRepository _basketReadRepository;

        public Basket Basket => ContextUser().Result;

        public BasketService(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IProductReadRepository productReadRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketReadRepository basketReadRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;

            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _productReadRepository = productReadRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketReadRepository = basketReadRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<Basket> ContextUser()
        {
            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if(userName is not null)
            {
              AppUser? user = await _userManager.Users.Include(x => x.Baskets).FirstOrDefaultAsync(u => u.UserName.Equals(userName));

                var _basket = from basket in user?.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into OrderBasket
                              from order in OrderBasket.DefaultIfEmpty()
                              select new
                              {
                                  basket = basket,
                                  order = order,
                              };
                Basket? targetBasket = null;
                if(_basket.Any(b => b.order is null))
                {
                    targetBasket = _basket.FirstOrDefault(x => x.order is null)?.basket;
                }
                else
                {
                    targetBasket = new Basket();
                    user?.Baskets.Add(targetBasket);
                }
                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception();
        }

        public async Task AddBasketItem(CreateBasketItemViewModel model)
        {
            Basket? basket = await ContextUser();
            Product? product =await _productReadRepository.GetByIdAsync(model.ProductId);

            BasketItem? basketItem =await _basketItemReadRepository.Table.FirstOrDefaultAsync(bi => bi.BasketId.Equals(basket.Id) && bi.ProductId.Equals(Guid.Parse(model.ProductId)));

            if(basketItem is not null)
            {
                basketItem.Quantity++;
            }
            else
            {
               await _basketItemWriteRepository.AddAsync(new()
                {
                    BasketId = basket.Id,
                    ProductId = Guid.Parse(model.ProductId),
                    Quantity = 1
                });
            }

           await _basketItemWriteRepository.SaveAsync();
        }

        public async Task<List<BasketItem>> GetBasketItems()
        {
            Basket? basket = await ContextUser();
            Basket? result = await _basketReadRepository.Table
                 .Include(b => b.BasketItems)
                 .ThenInclude(bi => bi.Product).ThenInclude(x => x.ProductImageFiles)
                 .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.ToList();
        }

        public async Task RemoveBasketItem(string basketItemId)
        {
            List<BasketItem> items = await GetBasketItems();

          BasketItem? item =  items.FirstOrDefault(i => i.Id == Guid.Parse(basketItemId));

          await  _basketItemWriteRepository.RemoveAsync(item.Id.ToString());
           await _basketItemWriteRepository.SaveAsync();
        }

        public async Task ChangeQuantityBasketItem(string basketItemId, int quantity)
        {
            List<BasketItem> items = await GetBasketItems();

            BasketItem? item = items.FirstOrDefault(i => i.Id == Guid.Parse(basketItemId));

            item.Quantity = quantity;
            await _basketItemWriteRepository.SaveAsync();
        }
    }
}
