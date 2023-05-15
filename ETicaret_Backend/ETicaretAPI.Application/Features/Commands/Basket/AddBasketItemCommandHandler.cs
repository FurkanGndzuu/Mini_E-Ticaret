using ETicaretAPI.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Basket
{
    public class AddBasketItemQueryHandler : IRequestHandler<AddBasketItemCommandRequest, AddBasketItemCommandResponse>
    {
        readonly IBasketService _basketService;

        public AddBasketItemQueryHandler(IBasketService basketSerice)
        {
            _basketService = basketSerice;
        }

        public async Task<AddBasketItemCommandResponse> Handle(AddBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
         await  _basketService.AddBasketItem(new()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });
            return new();
        }
    }
}
