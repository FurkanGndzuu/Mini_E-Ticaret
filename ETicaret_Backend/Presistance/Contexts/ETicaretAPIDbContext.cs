using ETicareAPI.Domain.Entities;
using ETicareAPI.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance.Contexts
{
    public class ETicaretAPIDbContext : IdentityDbContext<AppUser , AppRole , string>
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<ETicareAPI.Domain.Entities.File> Files { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach(var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow,
                };
            }


            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>().HasKey(x => x.Id);

            builder.Entity<Basket>().HasOne(b => b.Order).WithOne(o => o.Basket).HasForeignKey<Order>(x => x.Id);

            builder.Entity<Order>().HasOne(o => o.CompletedOrder).WithOne(co => co.Order).HasForeignKey<CompletedOrder>(co => co.OrderId);
        }


    }
}
