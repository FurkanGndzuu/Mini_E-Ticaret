using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Basket
{
    public class ListBasketItemQueryHandler : IRequestHandler<ListBasketItemQueryRequest, List<ListBasketItemQueryResponse>>
    {
        readonly IBasketService _basketService;

        public ListBasketItemQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<ListBasketItemQueryResponse>> Handle(ListBasketItemQueryRequest request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItems();

            return basketItems.Select(ba => new ListBasketItemQueryResponse
            {
                BasketItemId = ba.Id.ToString(),
                Name = ba.Product.Name,
                Price = ba.Product.Price,
                Quantity = ba.Quantity,
                path =  ba.Product?.ProductImageFiles?.FirstOrDefault(x => x.showCase == true)?.path
            }).ToList();
        }
    }
}
