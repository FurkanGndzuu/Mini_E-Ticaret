using ETicaretAPI.Application.Repositories.FileRepositories;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = ETicareAPI.Domain.Entities;

namespace ETicaretAPI.Presistance.Repositories.FileRepositories
{
    public class FileReadRepository : ReadRepository<F.File>, IFileReadRepository
    {
        public FileReadRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
