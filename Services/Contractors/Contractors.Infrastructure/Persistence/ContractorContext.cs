using Microsoft.EntityFrameworkCore;
using Contractors.Domain.Common;
using Contractors.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contractors.Infrastructure.Persistence
{
    public class ContractorContext : DbContext
    {
        public ContractorContext(DbContextOptions<ContractorContext> options) : base(options)
        {
        }

        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ContractorHistory> ContractorHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContractorHistory>()
                .HasOne(s => s.Contractor)
                .WithMany(l => l.ContractorHistories)
                .HasForeignKey(s => s.ContractorId)
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
