using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories.CompletedOrder;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Repositories.CompletedOrder
{
    public class CompletedOrderReadRepository : ReadRepository<ETicareAPI.Domain.Entities.CompletedOrder>, ICompletedOrderReadRepository
    {
        public CompletedOrderReadRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
