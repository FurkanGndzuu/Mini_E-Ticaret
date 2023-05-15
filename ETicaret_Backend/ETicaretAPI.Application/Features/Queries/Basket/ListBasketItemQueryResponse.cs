using ETicareAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Basket
{
    public class ListBasketItemQueryResponse
    {
        public string BasketItemId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public string? path { get; set; }
    }
}
