using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.GetCustomerById;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.Abstractions
{
    public interface ICustomerRepository
    {
        Task AddAsync(
            Domain.Features.Customers.Customer customer,
            CancellationToken cancellationToken);

        Task<GetCustomerByIdResult?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
