using ETicaretAPI.Application.Repositories.EndPoint;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Repositories.EndPoint
{
    public class EndPointWriteRepository : WriteRepository<ETicareAPI.Domain.Entities.EndPoint>, IEndPointWriteRepository
    {
        public EndPointWriteRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
