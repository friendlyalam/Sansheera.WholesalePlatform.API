using Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.Abstractions;
using Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.GetCustomerStatus;
using Sansheera.WholesalePlatform.Infrastructure.Persistence;
using Sansheera.WholesalePlatform.Infrastructure.Persistence.Dapper;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Customers
{
    public sealed class CustomerStatusRepository : ICustomerStatusRepository
    {
        private readonly IDapper _dapper;

        public CustomerStatusRepository(
            AppDbContext dbContext,
            IDapper dapper)
        {
            _dapper = dapper;
        }

        public Task<IReadOnlyList<CustomerStatusDto>> GetActiveAsync(
            CancellationToken cancellationToken)
        {
            const string sql = """
            SELECT
                id AS "Id",
                code AS "Code",
                name AS "Name",
                is_active AS "IsActive",
                created_at AS "CreatedAt"
            FROM customer_status
            WHERE is_active = TRUE
            ORDER BY id;
            """;

            return _dapper.QueryAsync<CustomerStatusDto>(
                sql,
                new { },
                cancellationToken);
        }

        public async Task<CustomerStatusDto?> GetByCodeAsync(
            string code,
            CancellationToken cancellationToken)
        {
            const string sql = """
            SELECT
                id AS "Id",
                code AS "Code",
                name AS "Name",
                is_active AS "IsActive",
                created_at AS "CreatedAt"
            FROM customer_status
            WHERE code = @Code;
            """;
            return await _dapper.QuerySingleOrDefaultAsync<CustomerStatusDto>(
                sql,
                new { Code = code },
                cancellationToken);
        }
    }
}
