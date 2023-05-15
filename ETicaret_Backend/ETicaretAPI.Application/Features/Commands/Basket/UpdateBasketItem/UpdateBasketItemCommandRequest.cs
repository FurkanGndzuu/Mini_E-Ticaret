using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Basket.UpdateBasketItem
{
    public class UpdateBasketItemCommandRequest : IRequest<UpdateBasketItemCommandResponse>
    {
        public string basketItemId { get; set; }
        public int quantity { get; set; }
    }
}
