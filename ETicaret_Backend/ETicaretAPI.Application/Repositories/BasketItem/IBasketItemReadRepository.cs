using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = ETicareAPI.Domain.Entities;

namespace ETicaretAPI.Application.Repositories.BasketItem
{
    public interface IBasketItemReadRepository : IReadRepository<F.BasketItem>
    {
    }
}
