using Microsoft.EntityFrameworkCore;
using Companies.Domain.Common;
using Companies.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Companies.Infrastructure.Persistence
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        //public DbSet<ProductHistory> ProductHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ProductHistory>()
            //    .HasOne(s => s.Product)
            //    .WithMany(l => l.ProductHistories)
            //    .HasForeignKey(s => s.ProductId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {

            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
