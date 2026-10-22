using Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.GetBusinessType;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.Abstractions
{
    public interface IBusinessTypeRepository
    {
        Task<IReadOnlyList<BusinessTypeDto>> GetActiveAsync(
            CancellationToken cancellationToken);
    }
}
