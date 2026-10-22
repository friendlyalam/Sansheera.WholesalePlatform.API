using Microsoft.EntityFrameworkCore;
using Npgsql;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Application.Common.Exceptions;

namespace Sansheera.WholesalePlatform.Infrastructure.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                var postgresException = ex.GetBaseException() as PostgresException;

                if (postgresException?.SqlState == PostgresErrorCodes.UniqueViolation
                    //&& 
                    //postgresException.ConstraintName
                    //    == DatabaseConstraintNames.CustomerPhoneNumberUnique
                        )
                {
                    throw new DuplicateRecordException();
                }

                throw;
            }
        }
    }
}
