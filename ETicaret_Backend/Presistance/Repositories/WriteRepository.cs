using ETicareAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        readonly ETicaretAPIDbContext context;

        public WriteRepository(ETicaretAPIDbContext _context)
        {
           context = _context;
        }

        public DbSet<T> Table => context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
          EntityEntry<T> en =  await Table.AddAsync(model);
            return en.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
              await Table.AddRangeAsync(model);
            return true;
        }

        public bool Remove(T model)
        {
           Table.Remove(model);
            return true;
        }

        public async Task<bool> RemoveAsync(string id)
        {
          T? model =  await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return Remove(model);
        }

        public bool RemoveRange(List<T> model)
        {
            Table.RemoveRange(model);
            return true;
        }

        public async Task<int> SaveAsync()
         => await context.SaveChangesAsync();

        public bool Update(T model)
        {
            Table.Update(model);
            return true;
        }
    }
}
