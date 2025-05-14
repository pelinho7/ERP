using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheService
{
    public static class CacheServiceRegistration
    {
        public static IServiceCollection AddCacheServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
            services.AddSingleton<ICacheService, DistributedCacheService>();
            return services;
        }
    }
}
