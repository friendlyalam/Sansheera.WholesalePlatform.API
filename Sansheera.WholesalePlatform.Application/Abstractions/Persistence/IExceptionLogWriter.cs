namespace Sansheera.WholesalePlatform.Application.Abstractions.Persistence
{

    public interface IExceptionLogWriter
    {
        Task WriteAsync(
            ExceptionLogEntry entry,
            CancellationToken cancellationToken = default);
    }
}
