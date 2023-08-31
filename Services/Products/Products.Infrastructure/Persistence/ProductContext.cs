using Microsoft.EntityFrameworkCore;
using Products.Domain.Common;
using Products.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Infrastructure.Persistence
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductHistory>()
                .HasOne(s => s.Product)
                .WithMany(l => l.ProductHistories)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
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
