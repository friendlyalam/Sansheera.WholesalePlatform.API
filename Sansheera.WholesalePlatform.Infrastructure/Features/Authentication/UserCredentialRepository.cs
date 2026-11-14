using Microsoft.EntityFrameworkCore;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Domain.Authentication;
using Sansheera.WholesalePlatform.Infrastructure.Persistence;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Authentication;

public sealed class UserCredentialRepository : IUserCredentialRepository
{
    private readonly AppDbContext _dbContext;

    public UserCredentialRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserCredential?> GetByUserAccountIdAsync(
        Guid userAccountId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.UserCredentials
            .FirstOrDefaultAsync(
                x => x.UserAccountId == userAccountId,
                cancellationToken);
    }

    public async Task AddAsync(
        UserCredential userCredential,
        CancellationToken cancellationToken)
    {
        await _dbContext.UserCredentials.AddAsync(
            userCredential,
            cancellationToken);
    }
}