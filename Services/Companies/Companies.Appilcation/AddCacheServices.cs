using CacheService;
using Companies.Appilcation.Contracts.Persistence;
using Companies.Application.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Companies.Appilcation
{
    public static class CacheServiceRegistration
    {
        public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            return CacheService.CacheServiceRegistration.AddCacheServices(services, configuration);
        }
    }
}
