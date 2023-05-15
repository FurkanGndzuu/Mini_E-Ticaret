using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryResponse
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Adress { get; set; }
        public string? Description { get; set; }
        public object BasketItems { get; set; }
        public bool Completed { get; set; }
    }
}
