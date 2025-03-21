﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Companies.Application.Contracts.Persistence;
using Companies.Infrastructure.Persistence;
using Companies.Infrastructure.Repositories;

namespace Companies.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<CompanyContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CompaniesConnectionString")));
            //potrzebne do mediatora
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));                        
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            return services;
        }
    }
}
