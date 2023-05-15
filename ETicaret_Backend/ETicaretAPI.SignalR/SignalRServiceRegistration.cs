using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR
{
    public static class SignalRServiceRegistration
    {
        public static void AddSignalRService(this IServiceCollection services)
        {
            services.AddTransient<IProductHubService, ProductHubService>();
            services.AddSignalR();
           
        }
    }
}
