using Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.Abstractions;
using Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.GetBusinessType;
using Sansheera.WholesalePlatform.Infrastructure.Persistence.Dapper;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Customers
{
    public sealed class BusinessTypeRepository : IBusinessTypeRepository
    {
        private readonly IDapper _dapper;

        public BusinessTypeRepository(
            IDapper dapper)
        {
            _dapper = dapper;
        }

        public Task<IReadOnlyList<BusinessTypeDto>> GetActiveAsync(
            CancellationToken cancellationToken)
        {
            const string sql = """
            SELECT
                id AS "Id",
                code AS "Code",
                name AS "Name",
                is_active AS "IsActive",
                created_at AS "CreatedAt"
            FROM business_type
            WHERE is_active = TRUE
            ORDER BY id;
            """;

            return _dapper.QueryAsync<BusinessTypeDto>(
                sql,
                null,
                cancellationToken);
        }
    }
}
