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
        public DbSet<CompanyUser> CompanyUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompanyUser>()
                .HasOne(s => s.Company)
                .WithMany(l => l.CompanyUsers)
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyHistory>()
                .HasOne(s => s.Company)
                .WithMany(l => l.CompanyHistories)
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyUserHistory>()
                .HasOne(s => s.CompanyUser)
                .WithMany(l => l.CompanyUserHistories)
                .HasForeignKey(s => s.CompanyUserId)
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
