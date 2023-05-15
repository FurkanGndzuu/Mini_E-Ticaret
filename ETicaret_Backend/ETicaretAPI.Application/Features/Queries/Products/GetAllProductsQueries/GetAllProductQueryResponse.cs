using ETicareAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.GetAllProductsQueries
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        public object? Products { get; set; }

    }
}
