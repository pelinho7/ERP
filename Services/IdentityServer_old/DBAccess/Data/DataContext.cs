using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class DataContext : IdentityDbContext<AppUser, IdentityRole<int>, int
    , IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>
    , IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options)
    : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //if (!optionsBuilder.IsConfigured)
            //{
            //    IConfigurationRoot configuration = new ConfigurationBuilder()
            //       .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsettings.json")
            //       .Build();
            //    var connectionString = configuration.GetConnectionString("IdentityConnectionString");
            //    optionsBuilder.UseSqlServer(connectionString);
            //}
        }
    }
}
