using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Repositories.ProductRepositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
