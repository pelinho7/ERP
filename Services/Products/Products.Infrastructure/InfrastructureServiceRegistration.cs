using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Contracts.Persistence;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Repositories;

namespace Products.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProductsConnectionString")));
            //potrzebne do mediatora
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));                        
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductHistoryRepository, ProductHistoryRepository>();

            return services;
        }
    }
}
