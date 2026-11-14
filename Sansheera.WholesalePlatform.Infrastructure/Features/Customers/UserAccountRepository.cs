using Microsoft.EntityFrameworkCore;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Domain.Features.Customers;
using Sansheera.WholesalePlatform.Infrastructure.Persistence;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Customers
{
    public sealed class UserAccountRepository : IUserAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public UserAccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(
            UserAccount userAccount,
            CancellationToken cancellationToken)
        {
            await _dbContext.UserAccounts.AddAsync(
                userAccount,
                cancellationToken);
        }

        public async Task<UserAccount?> GetActiveCustomerByPhoneNumberAsync(
    string phoneNumber,
    CancellationToken cancellationToken)
        {
            return await _dbContext.UserAccounts
                .FirstOrDefaultAsync(
                    x => x.PhoneNumber == phoneNumber
                        && x.UserType == "customer"
                        && x.IsActive,
                    cancellationToken);
        }
    }
}
