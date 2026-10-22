namespace Sansheera.WholesalePlatform.Application.Abstractions.Persistence
{
    public sealed record ExceptionLogEntry(
 string? TraceId,
 string LogLevel,
 string? ExceptionType,
 string Message,
 string? StackTrace,
 string? InnerException,
 string? RequestUrl,
 string? HttpMethod,
 Guid? LoginId,
 string? ClientIp,
 string? UserAgent,
 string? Environment,
 string? Payload);
}
