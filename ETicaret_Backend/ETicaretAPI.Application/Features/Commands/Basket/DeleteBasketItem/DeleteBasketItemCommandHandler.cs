using ETicaretAPI.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommandRequest, DeleteBasketItemCommandResponse>
    {
        readonly IBasketService _basketService;

        public DeleteBasketItemCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<DeleteBasketItemCommandResponse> Handle(DeleteBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.RemoveBasketItem(request.basketItemId);
            return new();
        }
    }
}
