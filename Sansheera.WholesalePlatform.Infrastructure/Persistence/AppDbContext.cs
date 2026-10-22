using Microsoft.EntityFrameworkCore;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Domain.Customers;
using Sansheera.WholesalePlatform.Domain.ExceptionLog;

namespace Sansheera.WholesalePlatform.Infrastructure.Persistence
{
    public sealed class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerStatus> CustomerStatuses => Set<CustomerStatus>();
        public DbSet<BusinessType> BusinessTypes => Set<BusinessType>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<ExceptionLog> ExceptionLogs => Set<ExceptionLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }
    }
}