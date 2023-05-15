using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Abstractions.Authentication;
using ETicaretAPI.Application.Abstractions.User;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Application.Repositories.BasketItem;
using ETicaretAPI.Application.Repositories.CompletedOrder;
using ETicaretAPI.Application.Repositories.CustomerRepositories;
using ETicaretAPI.Application.Repositories.EndPoint;
using ETicaretAPI.Application.Repositories.FileRepositories;
using ETicaretAPI.Application.Repositories.InvoiceFileRepositories;
using ETicaretAPI.Application.Repositories.OrderRepositories;
using ETicaretAPI.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using ETicaretAPI.Presistance.Contexts;
using ETicaretAPI.Presistance.Repositories.Basket;
using ETicaretAPI.Presistance.Repositories.BasketItem;
using ETicaretAPI.Presistance.Repositories.CompletedOrder;
using ETicaretAPI.Presistance.Repositories.CustomerRepositories;
using ETicaretAPI.Presistance.Repositories.EndPoint;
using ETicaretAPI.Presistance.Repositories.FileRepositories;
using ETicaretAPI.Presistance.Repositories.InvoiceFileRepositories;
using ETicaretAPI.Presistance.Repositories.Menu;
using ETicaretAPI.Presistance.Repositories.OrderRepositories;
using ETicaretAPI.Presistance.Repositories.ProductImageFileRepositories;
using ETicaretAPI.Presistance.Repositories.ProductRepositories;
using ETicaretAPI.Presistance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Presistance
{
    public static class ServiceRegiration
    {
        public static void AddPresistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETicaretAPIDbContext>(options 
                => options.UseNpgsql(Configuration.ConnectionString) , ServiceLifetime.Scoped);
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ETicaretAPIDbContext>();


            services.AddScoped<IExternalLogin, AuthService>();
            services.AddScoped<IInternalLogin, AuthService>();
            services.AddScoped<IAuth, AuthService>();
            services.AddScoped<IUserServices , UserService>();

            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketService, BasketService>();

            services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
            services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteReository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            services.AddScoped<IEndPointReadRepository, EndPointReadRepository>();
            services.AddScoped<IEndPointWriteRepository, EndPointWriteRepository>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRoleService , RoleService>();

            services.AddScoped<IAuthorizationEndPointService, AuthorizationEndPointService>();



        }
    }
}
