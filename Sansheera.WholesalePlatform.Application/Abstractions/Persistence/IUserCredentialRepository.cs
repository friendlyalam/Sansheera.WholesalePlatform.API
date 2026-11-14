using Sansheera.WholesalePlatform.Domain.Authentication;

namespace Sansheera.WholesalePlatform.Application.Abstractions.Persistence;

public interface IUserCredentialRepository
{
    Task<UserCredential?> GetByUserAccountIdAsync(
        Guid userAccountId,
        CancellationToken cancellationToken);

    Task AddAsync(
        UserCredential userCredential,
        CancellationToken cancellationToken);
}