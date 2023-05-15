using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.CustomerRepositories;
using ETicaretAPI.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Repositories.CustomerRepositories
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ETicaretAPIDbContext _context) : base(_context)
        {
        }
    }
}
