using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.Abstractions;
using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.Abstractions;
using Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.Abstractions;
using Sansheera.WholesalePlatform.Infrastructure.Features.Customers;
using Sansheera.WholesalePlatform.Infrastructure.Logging;
using Sansheera.WholesalePlatform.Infrastructure.Persistence;
using Sansheera.WholesalePlatform.Infrastructure.Persistence.Dapper;

namespace Sansheera.WholesalePlatform.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "DefaultConnection is missing.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddSingleton<NpgsqlDataSource>(
    _ => NpgsqlDataSource.Create(connectionString));

            services.AddScoped<ICustomerStatusRepository, CustomerStatusRepository>();
            services.AddScoped<IBusinessTypeRepository, BusinessTypeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDapper, DapperService>();

            //services.AddScoped<IUnitOfWork>(serviceProvider =>
            //    serviceProvider.GetRequiredService<AppDbContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExceptionLogWriter, ExceptionLogWriter>();

            return services;
        }
    }
}
