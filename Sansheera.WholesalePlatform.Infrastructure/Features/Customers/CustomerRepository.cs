using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.Abstractions;
using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.GetCustomerById;
using Sansheera.WholesalePlatform.Domain.Customers;
using Sansheera.WholesalePlatform.Infrastructure.Persistence;
using Sansheera.WholesalePlatform.Infrastructure.Persistence.Dapper;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Customers
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IDapper _dapper;

        public CustomerRepository(AppDbContext dbContext, IDapper dapper)
        {
            _dbContext = dbContext;
            _dapper = dapper;
        }

        public async Task AddAsync(
            Customer customer,
            CancellationToken cancellationToken)
        {
            await _dbContext.Customers.AddAsync(customer, cancellationToken);
        }
        

        public Task<GetCustomerByIdResult?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
        {
            const string sql = """
            SELECT
                id AS "Id",
                business_name AS "BusinessName",
                contact_person AS "ContactPerson",
                phone_number AS "PhoneNumber",
                email AS "Email",
                business_address AS "BusinessAddress",
                is_active AS "IsActive",
                created_at AS "CreatedAt"
            FROM customers
            WHERE id = @Id;
            """;

            return _dapper.QuerySingleOrDefaultAsync<GetCustomerByIdResult>(
                sql,
                new { Id = id },
                cancellationToken);
        }

    }
}
