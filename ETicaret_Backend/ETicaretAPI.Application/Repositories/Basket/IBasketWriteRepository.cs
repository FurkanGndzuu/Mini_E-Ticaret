using F = ETicareAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IBasketWriteRepository : IWriteRepository<F.Basket>
    {
    }
}
