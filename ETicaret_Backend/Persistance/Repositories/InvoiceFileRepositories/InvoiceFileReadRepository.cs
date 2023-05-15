using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories.InvoiceFileRepositories;
using ETicaretAPI.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Repositories.InvoiceFileRepositories
{
    public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
