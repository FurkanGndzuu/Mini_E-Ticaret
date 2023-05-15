using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Repositories.Menu
{
    public class MenuReadRepository : ReadRepository<ETicareAPI.Domain.Entities.Menu>, IMenuReadRepository
    {
        public MenuReadRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
