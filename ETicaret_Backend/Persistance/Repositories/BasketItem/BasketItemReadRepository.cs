using ETicaretAPI.Application.Repositories.BasketItem;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = ETicareAPI.Domain.Entities;

namespace ETicaretAPI.Presistance.Repositories.BasketItem
{
    public class BasketItemReadRepository : ReadRepository<F.BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
