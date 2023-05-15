using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.ViewModels.Baskets_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions
{
    public interface IBasketService
    {
        Task AddBasketItem(CreateBasketItemViewModel model);
        Task<List<BasketItem>> GetBasketItems();

        Task RemoveBasketItem(string basketItemId);
        Task ChangeQuantityBasketItem(string basketItemId, int quantity);

        public Basket Basket { get; }
    }
}
