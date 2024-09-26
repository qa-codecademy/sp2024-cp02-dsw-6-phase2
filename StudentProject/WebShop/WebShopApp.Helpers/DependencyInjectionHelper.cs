using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.DataAccess;
using WebShopApp.DataAccess.Implementation;
using WebShopApp.DataAccess.Interface;
using WebShopApp.Services.Implementation;
using WebShopApp.Services.Interface;

namespace WebShopApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WebAppDbContext>
                (x => x.UseSqlServer(connectionString));
        }


        public static void InjectRepository(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICartRepository, CartRepository>();   
           

        }
        public static void InjectServices(IServiceCollection services)
        {
            
            services.AddTransient<IProductService, ProductServices>();
            services.AddTransient<IUserService, UserServices>();
            services.AddTransient<ICartServices , CartServices>();
        }


    }
}
