using Sansheera.WholesalePlatform.Domain.Features.Customers;

namespace Sansheera.WholesalePlatform.Application.Abstractions.Persistence
{
    public interface IUserAccountRepository
    {
        Task AddAsync(
            UserAccount userAccount,
            CancellationToken cancellationToken);

        Task<UserAccount?> GetActiveCustomerByPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken);
    }
}
