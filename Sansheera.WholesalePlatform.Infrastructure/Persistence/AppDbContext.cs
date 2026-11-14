using Microsoft.EntityFrameworkCore;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Domain.Authentication;
using Sansheera.WholesalePlatform.Domain.ExceptionLog;
using Sansheera.WholesalePlatform.Domain.Features.Customers;

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
        public DbSet<UserAccount> UserAccounts => Set<UserAccount>();
        public DbSet<ExceptionLog> ExceptionLogs => Set<ExceptionLog>();
        public DbSet<UserCredential> UserCredentials => Set<UserCredential>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }
    }
}