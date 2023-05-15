using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = ETicareAPI.Domain.Entities;

namespace ETicaretAPI.Presistance.Repositories.Basket
{
    public class BasketReadRepository : ReadRepository<F.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
