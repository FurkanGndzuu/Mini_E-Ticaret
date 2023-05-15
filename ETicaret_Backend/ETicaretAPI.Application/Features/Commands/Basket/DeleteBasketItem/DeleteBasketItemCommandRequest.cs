using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandRequest : IRequest<DeleteBasketItemCommandResponse>
    {
        public string basketItemId { get; set; }
    }
}
