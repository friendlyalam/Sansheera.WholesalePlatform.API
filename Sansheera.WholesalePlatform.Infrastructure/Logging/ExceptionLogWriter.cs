using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Domain.ExceptionLog;
using Sansheera.WholesalePlatform.Infrastructure.Persistence;

namespace Sansheera.WholesalePlatform.Infrastructure.Logging
{
    public sealed class ExceptionLogWriter : IExceptionLogWriter
    {
        private readonly AppDbContext _dbContext;

        public ExceptionLogWriter(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task WriteAsync(
            ExceptionLogEntry entry,
            CancellationToken cancellationToken = default)
        {
            var exceptionLog = new ExceptionLog(
                entry.TraceId,
                entry.LogLevel,
                entry.ExceptionType,
                entry.Message,
                entry.StackTrace,
                entry.InnerException,
                entry.RequestUrl,
                entry.HttpMethod,
                entry.LoginId,
                entry.ClientIp,
                entry.UserAgent,
                entry.Environment,
                entry.Payload);

            await _dbContext.ExceptionLogs.AddAsync(
                exceptionLog,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
