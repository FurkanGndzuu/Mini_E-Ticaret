using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = ETicareAPI.Domain.Entities;

namespace ETicaretAPI.Presistance.Repositories.Basket
{
    public class BasketWriteRepository : WriteRepository<F.Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
