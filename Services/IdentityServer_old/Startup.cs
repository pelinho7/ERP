using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity;
using IdentityServer.Services;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddCors();

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnectionString")));

            services.AddIdentity<AppUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireDigit = false;
            })
.AddRoles<IdentityRole<int>>()
.AddSignInManager<SignInManager<AppUser>>()
.AddRoleManager<RoleManager<IdentityRole<int>>>()
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                //.AddTestUsers(TestUsers.Users)
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential();


            services.AddScoped<IProfileService, ProfileService>();
            //        services.AddAuthentication("Identity.Application")
            //.AddCookie("Identity.Application", options =>
            //{
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            //app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                });
            });
        }
    }
}
