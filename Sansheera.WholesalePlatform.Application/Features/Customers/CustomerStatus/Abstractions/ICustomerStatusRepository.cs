using Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.GetCustomerStatus;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.Abstractions
{
    public interface ICustomerStatusRepository
    {
        Task<IReadOnlyList<CustomerStatusDto>> GetActiveAsync(
            CancellationToken cancellationToken);
        Task<CustomerStatusDto?> GetByCodeAsync(string code, CancellationToken cancellationToken);
    }
}
